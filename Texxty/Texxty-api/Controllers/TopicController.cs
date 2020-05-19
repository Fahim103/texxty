using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Texxty_api.Attributes;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Repository;
using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/Topics")]
    public class TopicController : ApiController
    {
        // [IMPORTANT] GetTopicList MUST BE ACCESSIBLE BY ANY VALID LOGGED USER, EVEN IF NOT ADMIN 
        // TODO [DONE] : WIll need to Add Authorization of Admin only to create, update, delete post

        private bool IsUserAdmin()
        {
            return Thread.CurrentPrincipal.IsInRole("admin") ? true : false;
        }

        [Route("")]
        [HttpPost]
        [BearerAuthentication]
        public IHttpActionResult Post([FromBody]BlogTopic blogTopic)
        {
            if (!IsUserAdmin())
            {
                // If user isn't admin
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                ITopicRepository topicRepository = new TopicRepository();
                topicRepository.Insert(blogTopic);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, blogTopic));
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed To Create"));
            }

        }

        [Route("")]
        [HttpGet]
        [BearerAuthentication]
        public IHttpActionResult GetTopicList()
        {
            ITopicRepository topicRepository = new TopicRepository();
            var topicList = topicRepository.GetTopicsModelsList();
            if(topicList.Count > 0)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, topicList));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "No Topics exists in Database"));
        }
        

        [Route("{topic_id}")]
        [HttpGet]
        [BearerAuthentication]
        public IHttpActionResult GetTopicByID(int topic_id)
        {
            if (!IsUserAdmin())
            {
                // If user isn't admin
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            ITopicRepository topicRepository = new TopicRepository();
            var blogTopics = topicRepository.GetTopicsModelByID(topic_id);
            if (blogTopics != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, blogTopics));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "No Topics exists in Database"));
        }

        [Route("{topic_id}")]
        [HttpPut]
        [BearerAuthentication]

        public IHttpActionResult Put(int topic_id, [FromBody]BlogTopic blogTopic)
        {
            if (!IsUserAdmin())
            {
                // If user isn't admin
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            if (!ModelState.IsValid)
            {
                // IF Model State is invalid
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState));
            }

            ITopicRepository topicRepository = new TopicRepository();
            // Get Topic from Db
            try
            {
                var dbBlogTopic = topicRepository.Get(topic_id);
                if (dbBlogTopic != null)
                {
                    // Update the value in db
                    dbBlogTopic.Name = blogTopic.Name;
                    topicRepository.Update(dbBlogTopic);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
                } 
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Topic doesn't exist"));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to update topic name"));
            }
        }

        [Route("{topic_id}")]
        [HttpDelete]
        [BearerAuthentication]
        public IHttpActionResult Delete(int topic_id)
        {
            if (!IsUserAdmin())
            {
                // If user isn't admin
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            ITopicRepository topicRepository = new TopicRepository();
            IBlogRepository blogRepository = new BlogRepository();
            // Get Topic from Db
            try
            {
                var dbBlogTopic = topicRepository.Get(topic_id);
                if (dbBlogTopic != null)
                {
                    var blogsExistWithTopicID = blogRepository.GetAll().Where(x => x.TopicID == topic_id);
                    if(blogsExistWithTopicID.Any())
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.MethodNotAllowed, "Failed to delete. One or multiple blogs have set this topic as blog topic"));
                    }
                    topicRepository.Delete(dbBlogTopic.BlogTopicID);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Topic doesn't exist"));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to update topic name"));
            }
        }
    }
}
