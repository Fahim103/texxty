using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Slugify;
using Texxty_api.Attributes;
using TexxtyDataAccess.Models;

using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/Posts")]

    public class PostsController : ApiController
    {
        private readonly PostRepository postrepo = new PostRepository();
        private readonly SlugHelper helper = new SlugHelper();

        [HttpGet]
        [BearerAuthentication]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var posts = postrepo.GetPostModelList(id);
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
        [Route("{id}")]
        [HttpPost]
        [BearerAuthentication]
        public HttpResponseMessage Post([FromUri] int id, [FromBody]Post post)
        {
            try
            {
                post.BlogID = id;
                post.ViewCount = 0;
                    post.UrlField = helper.GenerateSlug(post.Title.ToString());
                    postrepo.Insert(post);
                

                var resp = Request.CreateResponse(HttpStatusCode.Created);
                resp.Headers.Location = new Uri(new Uri(Request.RequestUri, ".") + post.PostID.ToString());

                return resp;
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create new post."+e);
            }
        }
        [Route("Edit/{id}")]
        [HttpPut]
        [BearerAuthentication]
        public HttpResponseMessage Put([FromBody]Post post, [FromUri]int id)
        { try
            {

                post.PostID = id;
                Post entity = postrepo.Get(id);
                entity.PostContent = post.PostContent;
                entity.ModifiedDate = DateTime.Now;
                entity.Title = post.Title;
                entity.ViewCount = post.ViewCount;
                entity.Draft = post.Draft;
                entity.UrlField = helper.GenerateSlug(post.Title.ToString());
                postrepo.Update(entity);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to edit post.");

            }

        }
        [Route("Delete/{id}")]
        [BearerAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                postrepo.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to edit post.");

            }

        }
        [Route("Details/{id}")]
        [BearerAuthentication]
        [HttpGet]
        public HttpResponseMessage GetDetails(int id)
        {
            try
            {
                var post = postrepo.GetPostModel(id);

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