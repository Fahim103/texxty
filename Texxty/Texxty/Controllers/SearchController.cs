using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository.Classes;
using Texxty.Models.ViewModel;

namespace Texxty.Controllers
{
    public class SearchController : Controller
    {
        private readonly TexxtyDBEntities context;
        private readonly BlogRepository blogRepository;
        private readonly PostRepository postRepository;

        public SearchController()
        {
            context = new TexxtyDBEntities();
            blogRepository = new BlogRepository();
            postRepository = new PostRepository();
        }


        [HttpGet]
        public ActionResult SearchContents(string searchText)
        {
            var blogMatch = context.Blogs.Where(b => b.Title.Contains(searchText)).Where(b => b.Private == false).ToList();
            var postMatch = context.Posts.Where(p => p.Blog.Private == false).Where((p => p.Title.Contains(searchText) || p.PostContent.Contains(searchText))).Where(p => p.Draft == false).ToList();

            SearchViewModel model = new SearchViewModel
            {
                Blogs = new List<Blog>(),
                Posts = new List<Post>()
            };
            
            if (blogMatch.Count > 0)
            {
                for (var i = 0; i < blogMatch.Count; i++)
                {
                    var item = blogRepository.Get(blogMatch[i].BlogID);
                    model.Blogs.Add(item);
                }
            }

            if (postMatch.Count > 0)
            {
                for (var i = 0; i < postMatch.Count; i++)
                {
                    var item = postRepository.Get(postMatch[i].PostID);
                    model.Posts.Add(item);
                }
            }

            return View(model);
        }
    }
}