using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class BlogRepository : Repository<Blog>
    {
        private readonly TexxtyDBEntities context;

        public BlogRepository()
        {
            this.context = new TexxtyDBEntities();
        }

        public bool CheckBlogPrivate(int id) =>
            context.Blogs.Where(b => b.BlogID == id).FirstOrDefault().Private;

        public List<Blog> GetAllBlogsByID(int id)
        {
            return context.Blogs.Where(x => x.UserID == id).ToList();
        }
    }
}