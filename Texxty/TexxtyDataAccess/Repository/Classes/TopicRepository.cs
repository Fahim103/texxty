using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TexxtyDataAccess.Models;

namespace TexxtyDataAccess.Repository.Classes
{
    public class TopicRepository : Repository<BlogTopic>, ITopicRepository
    {
        private readonly TexxtyDBEntities context;

        public TopicRepository()
        {
            this.context = new TexxtyDBEntities();
        }
    }
}