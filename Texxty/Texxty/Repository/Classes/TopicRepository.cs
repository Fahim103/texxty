using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class TopicRepository : Repository<BlogTopic>
    {
        private readonly TexxtyDBEntities context;

        public TopicRepository()
        {
            this.context = new TexxtyDBEntities();
        }
    }
}