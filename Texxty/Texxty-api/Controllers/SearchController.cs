using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TexxtyDataAccess.Models.Utilities;
using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{
    public class SearchController : ApiController
    {

        [Route("api/Search/{search}")]
        public IHttpActionResult GetSearchContents(string search)
        {
            SearchUtility _searchUtility = new SearchUtility();
            return Ok(_searchUtility.GetBlogAndPostList(search));
        }

        [Route("api/Search/{entity}/{search}")]
        public IHttpActionResult GetSearchContents(string entity, string search)
        {
            SearchUtility _searchUtility = new SearchUtility();

            if (entity.ToLower().Equals("blog"))
            {
                return Ok(_searchUtility.GetBlogList(search));
            }
            else if (entity.ToLower().Equals("post"))
            {
                return Ok(_searchUtility.GetPostList(search));
            }
            else
            {
                return Ok(_searchUtility.GetBlogAndPostList(search));
            }
        }

        [Route("api/Search/Blogs/{blog_id}")]
        [HttpGet]
        public IHttpActionResult GetBlogPostsByID(int blog_id)
        {
            try
            {
                PostRepository postRepository = new PostRepository();
                var posts = postRepository.GetSearchPostModelList(blog_id);
                if (posts == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));

                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, posts));

            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not find the  posts you are looking for."));
            }
        }
    }
}
