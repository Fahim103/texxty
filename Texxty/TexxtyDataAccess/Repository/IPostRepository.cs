using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexxtyDataAccess.Models;

namespace TexxtyDataAccess.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        void DeleteByBlog(int blogId);
        List<Post> GetAllPosts(int id);
        List<Post> GetAllPublicPosts();
        List<Post> GetAllPostsByTopic(int topicId);
        void CountViews(int postid);
        List<Post> GetAllPostByTopicFollow(int user_id);
    }
}
