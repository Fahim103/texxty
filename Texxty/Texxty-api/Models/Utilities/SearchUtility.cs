using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Texxty_api.Models.CustomModels;
using Texxty_api.Repository.Classes;

namespace Texxty_api.Models.Utilities
{
    public class SearchUtility
    {
        private readonly TexxtyDBEntities context;
        private readonly BlogRepository blogRepository;
        private readonly PostRepository postRepository;
        private List<BlogModel> blogList;
        private List<PostModel> postList;

        public SearchUtility()
        {
            context = new TexxtyDBEntities();
            blogRepository = new BlogRepository();
            postRepository = new PostRepository();
        }

        /// <summary>
        /// Returns distinct list of Blogs and distinct list of Posts depending on Search String
        /// </summary>
        /// <param name="search">String which is used for searching</param>
        /// <returns></returns>
        public List<Object> GetBlogAndPostList(string search)
        {
            List<object> blogAndPostList = new List<object>();

            blogAndPostList.Add(this.GetBlogList(search));
            blogAndPostList.Add(this.GetPostList(search));

            return blogAndPostList.Distinct().ToList();
        }

        /// <summary>
        /// Returns distinct list of Blogs depending on Search String
        /// </summary>
        /// <param name="search">String which is used for searching</param>
        /// <returns></returns>
        public List<BlogModel> GetBlogList(string search)
        {

            blogList = new List<BlogModel>();
            string[] searchString = search.Split(' ');
            for (int s = 0; s < searchString.Length; s++)
            {
                string searchText = searchString[s];
                var blogMatch = context.Blogs.Where(b => b.Title.Contains(searchText)).Where(b => b.Private == false).ToList();

                if (blogMatch.Count > 0)
                {
                    for (var i = 0; i < blogMatch.Count; i++)
                    {
                        var item = blogRepository.Get(blogMatch[i].BlogID);
                        blogList.Add(new BlogModel()
                        {
                            BlogID = item.BlogID,
                            Title = item.Title,
                            Description = item.Description,
                            UrlField = item.UrlField,
                            Private = item.Private,
                            UserID = item.UserID,
                            TopicID = item.TopicID,
                            ViewCount = item.ViewCount
                        });
                    }
                }
            }

            return blogList.DistinctBy(x => x.BlogID).ToList();
        }

        /// <summary>
        /// Returns distinct list of Posts depending on Search String
        /// </summary>
        /// <param name="search">String which is used for searching</param>
        /// <returns></returns>
        public List<PostModel> GetPostList(string search)
        {
            postList = new List<PostModel>();
            string[] searchString = search.Split(' ');
            for (int s = 0; s < searchString.Length; s++)
            {
                string searchText = searchString[s];
                var postMatch = context.Posts.Where(p => p.Blog.Private == false).Where((p => p.Title.Contains(searchText) || p.PostContent.Contains(searchText))).Where(p => p.Draft == false).ToList();

                if (postMatch.Count > 0)
                {
                    for (var i = 0; i < postMatch.Count; i++)
                    {
                        var item = postRepository.Get(postMatch[i].PostID);
                        postList.Add(new PostModel()
                        {
                            PostID = item.PostID,
                            Title = item.Title,
                            PublishedDate = item.PublishedDate,
                            ModifiedDate = item.ModifiedDate,
                            UrlField = item.UrlField,
                            Draft = item.Draft,
                            PostContent = item.PostContent,
                            BlogID = item.BlogID,
                            ViewCount = item.ViewCount
                        });
                    }
                }
            }
            
            return postList.DistinctBy(x => x.PostID).ToList();
        }
    }
}