using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository.Classes
{
    public class FollowTopicRepository : Repository<TopicFollow>, IFollowTopicRepository
    {
        private readonly TexxtyDBEntities context;

        public FollowTopicRepository()
        {
            this.context = new TexxtyDBEntities();
        }

        public List<TopicFollow> GetTopicsByUser(int user_id) =>
            context.TopicFollows.Where(tf => tf.UserID == user_id).ToList();
        public List<TopicFollowModel> GetTopicsByUserModel(int user_id)
        {
            var topicfollow =context.TopicFollows.Where(tf => tf.UserID == user_id).ToList();
            var topicFollowModel = new List<TopicFollowModel>();
            foreach (TopicFollow topic in topicfollow)
            {
                topicFollowModel.Add(new TopicFollowModel()
                {
                   TopicFollowID=  topic.TopicFollowID,
                   TopicID= topic.TopicID,
                   UserID= topic.UserID


                });

            }
            return topicFollowModel;

        }
        public void DeleteByUser(int user_id)
        {
            var topics = context.TopicFollows.Where(tf => tf.UserID == user_id);
            
            foreach (var topic in topics)
            {
                context.TopicFollows.Remove(topic);
            }

            context.SaveChanges();
        }
        

        public void AddTopics(int user_id, List<string> topicList)
        {
            
            foreach (string id in topicList)
            {
                int topic_id = int.Parse(id);
               var  topicfollow= context.TopicFollows.Add(new TopicFollow
                {
                    UserID = user_id,
                    TopicID = topic_id
                });
            }

            context.SaveChanges();

            
        }
    }
}