using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Texxty_api.Attributes;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{

    [RoutePrefix("api")]
    public class BlogController : ApiController
    {
        private readonly BlogRepository  blogrepo = new BlogRepository();
        private readonly SlugHelper helper = new SlugHelper();

        [Route("Users/{user_id}/Blogs")]
        [BearerAuthentication]
        [HttpGet]
        public IHttpActionResult GetAllBlogsByUserID(int user_id)
        {
            try
            {
                var blogs = blogrepo.GetBlogModelList(user_id);
                if (blogs.Count == 0)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "No Blogs Exists Yet"));

                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, blogs));
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong"));
            }
        }

        [Route("Users/{user_id}/Blogs/{blog_id}")]
        [BearerAuthentication]
        [HttpGet]
        public IHttpActionResult GetBlog(int user_id, int blog_id)
        {
            try
            {
                var blog = blogrepo.GetBlogModel(user_id, blog_id);
                if (blog == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, blog));
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong"));
            }
        }

        [Route("Blogs")]
        [BearerAuthentication]
        [HttpGet]
        public HttpResponseMessage Get()
        {

            try
            {
                var blogs = blogrepo.GetBlogModelList();
                if (blogs.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Blogs not found");

                }
                return Request.CreateResponse(HttpStatusCode.OK, blogs);

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "" + e);
            }
        }

        [Route("Blogs/{blog_id}")]
        [BearerAuthentication]
        [HttpGet]
        public HttpResponseMessage Get(int blog_id)
        {
            try
            {
                var blog = blogrepo.GetBlogModel(blog_id);

                if (blog == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, blog);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not find the specified blog.");
            }
        }

        [Route("Blogs")]
        [HttpPost]
        [BearerAuthentication]
        public HttpResponseMessage Post([FromBody]Blog blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string errorMessages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessages);
                }
                else
                {
                    blog.ViewCount = 0;

                    blog.UrlField = helper.GenerateSlug(blog.Title.ToString());
                    blogrepo.Insert(blog);


                    var resp = Request.CreateResponse(HttpStatusCode.Created);
                    resp.Headers.Location = new Uri(new Uri(Request.RequestUri, ".") + blog.BlogID.ToString());

                    return resp;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create new blog." + e);
            }
        }
        [Route("Blogs/{blog_id}")]
        [HttpPut]
        [BearerAuthentication]
        public HttpResponseMessage Put([FromBody]Blog blog, [FromUri]int blog_id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string errorMessages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessages);
                }
                else
                {
                    Blog entity = blogrepo.Get(blog_id);
                    entity.Title = blog.Title;
                    entity.Description = blog.Description;
                    entity.Private = blog.Private;
                    entity.UrlField = helper.GenerateSlug(blog.Title.ToString());
                    blogrepo.Update(entity);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }

            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to edit post.");

            }
        }

        // Method in which blogViewCount is incremented when viewd from search result
        [Route("Blogs/{blog_id}/UpdateViewCount")]
        [HttpPut]
        public IHttpActionResult UpdateBlogViewCount([FromUri]int blog_id, [FromBody]int? userID)
        {
            try
            {
                Blog blog = blogrepo.Get(blog_id);
                if (blog.UserID != userID)
                {
                    blog.ViewCount = blog.ViewCount + 1;
                    blogrepo.Update(blog);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Blog View count wasn't updated"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Accepted));
        }


        [Route("Blogs/{blog_id}")]
        [BearerAuthentication]
        public HttpResponseMessage Delete(int blog_id)
        {
            try
            {
                PostRepository pr = new PostRepository();
                pr.DeleteByBlog(blog_id);
                blogrepo.Delete(blog_id);

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete post.");

            }
        }
    }
}
