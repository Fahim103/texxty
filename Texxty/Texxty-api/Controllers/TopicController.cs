using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Texxty_api.Attributes;
using TexxtyDataAccess.Repository;
using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/Topics")]
    public class TopicController : ApiController
    {
        [Route("")]
        [HttpGet]
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
    }
}
