using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Texxty_api.Attributes;
using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/FollowTopics/{user_id}")]
    public class FollowTopicController : ApiController
    {
        private readonly FollowTopicRepository ftrepo = new FollowTopicRepository();

        [Route("")]
        [BearerAuthentication]
        [HttpGet]
        public HttpResponseMessage GetTopicByUser(int user_id)
        {
            try 
            {
                var followTopic = ftrepo.GetTopicsByUserModel(user_id);
                if (followTopic.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Follow topics not found");

                }
                return Request.CreateResponse(HttpStatusCode.OK, followTopic);


            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "" + e);
            }
        }

        [Route("")]
        [HttpDelete]
        [BearerAuthentication]
        public HttpResponseMessage DeleteTopicByUser(int user_id)
        {
            try
            {
                ftrepo.DeleteByUser(user_id);

                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete the following topics.");
            }
        }

        [Route("{follow_topic_id}")]
        [HttpDelete]
        [BearerAuthentication]
        public HttpResponseMessage DeleteSpecificFollowedTopic(int user_id, int follow_topic_id)
        {
            try
            { 
                ftrepo.Delete(follow_topic_id);

                return Request.CreateResponse(HttpStatusCode.NoContent);

            }
            catch (Exception e)
            { 
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete specific topic" + e);
            }        
        }

        [Route("")]
        [BearerAuthentication]
        [HttpPost]
        public HttpResponseMessage PostTopics(int user_id, [FromBody] List<string> topicList)
        {
            try
            {
                if (topicList.Count >= 1)
                {
                    ftrepo.AddTopics(user_id, topicList);
                    var resp = Request.CreateResponse(HttpStatusCode.Created);
                    //resp.Headers.Location = new Uri(new Uri(Request.RequestUri, ".") + entity.TopicFollowID.ToString());
                    return resp;
                }

                else
                {
                    // TODO: Maybe return something else instead of no content
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No topics added");
                }

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create new topics." + e);
            }
        }
    }
}
