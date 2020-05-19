using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class TopicRepository : Repository<BlogTopic>, ITopicRepository
    {
        private readonly TexxtyDBEntitiesMVC context;

        public TopicRepository()
        {
            this.context = new TexxtyDBEntitiesMVC();
        }
    }
}