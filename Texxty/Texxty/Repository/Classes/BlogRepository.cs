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
        public void CountViews(int blogid)
        {
            var viewcount = context.Blogs.Where(u => u.BlogID == blogid).Select(p => p.ViewCount).ToArray();
            viewcount[0]++;
            var col = context.Blogs.Where(u => u.BlogID == blogid);
            foreach (var item in col)
            { item.ViewCount = viewcount[0]; }
            context.SaveChanges();



        }

        public bool CheckBlogPrivate(int id) =>
            context.Blogs.Where(b => b.BlogID == id).FirstOrDefault().Private;

        public List<Blog> GetAllBlogsByID(int id)
        {
            return context.Blogs.Where(x => x.UserID == id).ToList();
        }
    }
}