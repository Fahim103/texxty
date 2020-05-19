using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models.CustomModels
{
    public class TopicFollowModel
    {
        public int TopicID { get; set; }
        public int TopicFollowID { get; set; }
        public int UserID { get; set; }

        public List<string> Resources = new List<string>
            {
                "[GET]      /api/FollowTopics/{user_id}",
                "[DELETE]   /api/FollowTopics/{user_id}",
                "[DELETE]   /api/FollowTopics/{user_id}/{follow_topic_id}",
                "[POST]     /api/FollowTopics/{user_id}",
            };
    }
}
