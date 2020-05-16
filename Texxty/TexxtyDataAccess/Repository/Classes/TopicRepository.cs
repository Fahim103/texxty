using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository.Classes
{
    public class TopicRepository : Repository<BlogTopic>, ITopicRepository
    {
        private readonly TexxtyDBEntities context;

        public TopicRepository()
        {
            this.context = new TexxtyDBEntities();
        }

        public List<BlogTopicsModel> GetTopicsModelsList()
        {
            var entity = GetAll();
            var blogTopicsModel = new List<BlogTopicsModel>();
            foreach (BlogTopic blogTopic in entity)
            {
                blogTopicsModel.Add(new BlogTopicsModel()
                {
                    BlogTopicID = blogTopic.BlogTopicID,
                    Name = blogTopic.Name
                });

            }
            return blogTopicsModel;
        }
    }
}