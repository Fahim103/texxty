using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository
{
    public interface IFollowTopicRepository : IRepository<TopicFollow>
    {
        List<TopicFollow> GetTopicsByUser(int user_id);
        void DeleteByUser(int user_id);
        void AddTopics(int user_id, List<string> topicList);
        List<TopicFollowModel> GetTopicsByUserModel(int user_id);
    }
}
