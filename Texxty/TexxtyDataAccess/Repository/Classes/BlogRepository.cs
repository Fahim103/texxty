using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository.Classes
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
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
            { 
                item.ViewCount = viewcount[0]; 
            }
            context.SaveChanges(); // TODO : WHY IS THIS NEEDED  ?? 
        }
        public List<BlogModel> GetBlogModelList()
        {
            var entity = GetAll();
            var blogmodel = new List<BlogModel>();
            foreach (Blog blog in entity)
            {
                blogmodel.Add(new BlogModel()
                {
                    BlogID = blog.BlogID,
                    Description = blog.Description,
                    Private = blog.Private,
                    Title= blog.Title,
                    TopicID= blog.TopicID,
                    UrlField = blog.UrlField,
                    UserID= blog.UserID,
                    ViewCount= blog.ViewCount
                });

            }
            return blogmodel;
        }

        public List<BlogModel> GetBlogModelList(int user_id)
        {
            var entity = this.GetAllBlogsByUserID(user_id);
            var blogmodel = new List<BlogModel>();
            foreach (Blog blog in entity)
            {
                blogmodel.Add(new BlogModel()
                {
                    BlogID = blog.BlogID,
                    Description = blog.Description,
                    Private = blog.Private,
                    Title = blog.Title,
                    TopicID = blog.TopicID,
                    UrlField = blog.UrlField,
                    UserID = blog.UserID,
                    ViewCount = blog.ViewCount
                });
            }
            return blogmodel;
        }

        public BlogModel GetBlogModel(int id)
        {
            var entity = Get(id);
            return new BlogModel
            {
                BlogID = entity.BlogID,
                Description= entity.Description,
                Private = entity.Private,
                Title= entity.Title,
                TopicID= entity.TopicID,
                UrlField= entity.UrlField,
                UserID= entity.UserID,
                ViewCount= entity.ViewCount
            };
        }

        // cross check if blogs userid and provided used id are same or not
        public BlogModel GetBlogModel(int user_id, int blog_id)
        {
            var entity = Get(blog_id);
            if(entity.UserID == user_id)
            {
                return new BlogModel
                {
                    BlogID = entity.BlogID,
                    Description = entity.Description,
                    Private = entity.Private,
                    Title = entity.Title,
                    TopicID = entity.TopicID,
                    UrlField = entity.UrlField,
                    UserID = entity.UserID,
                    ViewCount = entity.ViewCount
                };
            }
            else
            {
                return null;
            }
        }

        public bool CheckBlogPrivate(int id) =>
            context.Blogs.Where(b => b.BlogID == id).FirstOrDefault().Private;

        public List<Blog> GetAllBlogsByUserID(int user_id)
        {
            return context.Blogs.Where(x => x.UserID == user_id).ToList();
        }
    }
}