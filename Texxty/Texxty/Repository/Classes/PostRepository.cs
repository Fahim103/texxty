using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly TexxtyDBEntitiesMVC context;

        public PostRepository()
        {
            this.context = new TexxtyDBEntitiesMVC();
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

        public void  CountViews(int postid)
        {
            var viewcount= context.Posts.Where(u => u.PostID == postid).Select(p => p.ViewCount).ToArray();
            viewcount[0]++;
            var col = context.Posts.Where(u => u.PostID == postid);
            foreach (var item in col)
            {


                 item.ViewCount  = viewcount[0]; } 
                context.SaveChanges();

        }
        public List<Post> GetAllPostByTopicFollow(int user_id)
        {
            var followTopicRepository = new FollowTopicRepository();
            PostRepository postRepository = new PostRepository();
            List<TopicFollow> topicFollows =  followTopicRepository.GetTopicsByUser(user_id);
            List<Post> result = new List<Post>();

            foreach(var topic in topicFollows)
            {
                var posts = context.Posts.Where(x => x.Blog.Private == false).Where(x => x.Draft == false).Where(x => x.Blog.TopicID == topic.TopicID).ToList();
                if(posts.Count > 0)
                {
                    for(var i = 0; i<posts.Count; i++)
                    {
                        var item = postRepository.Get(posts[i].PostID);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        
    }
}