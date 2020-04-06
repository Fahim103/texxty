using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texxty_api.Models;

namespace Texxty_api.Repository
{
    public interface IFollowTopicRepository : IRepository<TopicFollow>
    {
        List<TopicFollow> GetTopicsByUser(int user_id);
        void DeleteByUser(int user_id);
        void AddTopics(int user_id, List<string> topicList);
    }
}
