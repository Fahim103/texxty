using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class PostRepository : Repository<Post>
    {
        private readonly TexxtyDBEntities context;

        public PostRepository()
        {
            this.context = new TexxtyDBEntities();
        }

        public void DeleteByBlog(int blogId)
        {
            var postList = context.Posts.Where(p => p.BlogID == blogId).ToList();
            foreach (var post in postList)
            {
                context.Posts.Remove(post);
            }
            context.SaveChanges();
        }

        public List<Post> GetAllPosts(int id) =>
            context.Posts.Where(u => u.BlogID == id).ToList();

        public List<Post> GetAllPublicPosts() =>
            context.Posts.Where(u => u.Blog.Private == false).Where(u => u.Draft == false).ToList();

        public List<Post> GetAllPostsByTopic(int topicId) =>
            context.Posts.Where(u => u.Blog.TopicID == topicId).ToList();
    }
}