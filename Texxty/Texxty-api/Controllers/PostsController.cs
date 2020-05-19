using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.DynamicData;
using System.Web.Http;
using Slugify;
using TexxtyDataAccess.Repository.Classes;
using Texxty_api.Attributes;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Repository;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/Blogs/{blog_id}/Posts")]

    public class PostsController : ApiController
    {
        private readonly PostRepository postrepo = new PostRepository();
        private readonly SlugHelper helper = new SlugHelper();

        
        [Route("")]
        [HttpGet]
        [BearerAuthentication]
        public HttpResponseMessage Get(int blog_id)
        {
            try
            {
                var posts = postrepo.GetPostModelList(blog_id);
                if (posts.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                }
                return Request.CreateResponse(HttpStatusCode.OK, posts);

            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
               "Could not find the  posts you are looking for.");
            }


        }
        [Route("")]
        [HttpPost]
        [BearerAuthentication]
        public HttpResponseMessage Post([FromUri] int blog_id, [FromBody]Post post)
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
                    post.BlogID = blog_id;
                    post.ModifiedDate = DateTime.Now;
                    post.PublishedDate = DateTime.Now;
                    post.ViewCount = 0;
                    post.UrlField = helper.GenerateSlug(post.Title.ToString());
                    postrepo.Insert(post);


                    var resp = Request.CreateResponse(HttpStatusCode.Created);
                    resp.Headers.Location = new Uri(new Uri(Request.RequestUri, ".") + post.PostID.ToString());

                    return resp;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create new post." + e);
            }
        }

        [Route("{post_id}")]
        [HttpPut]
        [BearerAuthentication]
        public HttpResponseMessage Put([FromBody]Post post, [FromUri]int post_id)
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
                    // TODO : Check the comments
                    // post.PostID = post_id; // This line isn't needed as well probably 
                    Post entity = postrepo.Get(post_id);
                    if(entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to edit post.");
                    entity.PostContent = post.PostContent;
                    entity.ModifiedDate = DateTime.Now;
                    entity.Title = post.Title;
                    // entity.ViewCount = post.ViewCount; // This is proabably not needed here as this method is used by user to update post
                    entity.Draft = post.Draft;
                    entity.UrlField = helper.GenerateSlug(post.Title.ToString());
                    postrepo.Update(entity);
                    return Request.CreateResponse(HttpStatusCode.OK, postrepo.GetPostModel(post_id));
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to edit post.");

            }

        }

        [Route("{post_id}/UpdateViewCount")]
        [HttpPut]
        public HttpResponseMessage UpdateViewCount([FromUri]int post_id, [FromBody]int userID)
        {
            try
            {
                Post post = postrepo.Get(post_id);
                if(userID == 0)
                {
                    post.ViewCount = post.ViewCount + 1;
                    postrepo.Update(post);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    IBlogRepository blogRespository = new BlogRepository();
                    int dbUserID = blogRespository.GetUserIDByBlogID(post.BlogID);
                    if(dbUserID != userID)
                    {
                        post.ViewCount = post.ViewCount + 1;
                        postrepo.Update(post);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted);
                    }
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update count.");

            }

        }

        [Route("{post_id}")]
        [BearerAuthentication]
        public HttpResponseMessage Delete(int post_id)
        {
            try
            {
                postrepo.Delete(post_id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete post.");

            }

        }
        [Route("{post_id}")]
        [BearerAuthentication]
        [HttpGet]
        public HttpResponseMessage Get(int blog_id, int post_id)
        {
            try
            {
                var post = postrepo.GetPostModel(post_id);

                if (post == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, post);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Could not find the specified post.");
            }


        }
        // This method is for view only from feed to post details
       // [Route("Feed/{id}")]
       // [BearerAuthentication]
        //[HttpGet]
        /*  public HttpResponseMessage PostDetails(int id)
          {
           try{
              postrepo.CountViews(id);


              var entity = postrepo.GetPostModel(id);
              var blogId = entity.BlogID;
              var blogRepo = new BlogRepository();
              blogRepo.CountViews(blogId);
              var blogInfo = blogRepo.Get(blogId);
              var UserInfo = blogInfo.User.FullName;
              return Request.CreateResponse(HttpStatusCode.OK, entity);
              }
               catch
              {
                  return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Could not find the specified post.");
              }
          }*/
    }

}
