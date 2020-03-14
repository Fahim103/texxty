using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository.Classes;

namespace Texxty.Controllers
{
    public class HomeController : Controller
    {
        [NonAction]
        public static string WithMaxLength(string value, int maxLength)
        {
            if (value.Length < maxLength)
                return value.Substring(0, Math.Min(value.Length, maxLength));
            else
                return value.Substring(0, Math.Min(value.Length, maxLength)) + "...";
        }

        private readonly PostRepository postRepository = new PostRepository();

        [NonAction]
        public bool AuthorizeUser()
        {
            return Session["user_id"] != null;
        }

        public ActionResult Index()
        {
            //if (!AuthorizeUser())
            //    return RedirectToAction("Login", "Accounts");

            //return RedirectToAction("Feed");
            return View();
        }

        public ActionResult Feed()
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            var user_id = int.Parse(Session["user_id"].ToString());

            PostRepository postRepository = new PostRepository();
            var posts = postRepository.GetAllPostByTopicFollow(user_id);

            foreach (var post in posts)
            {
                post.PostContent = WithMaxLength(post.PostContent, 500);
            }

            return View(posts);
        }

        public ActionResult FollowTopic()
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            var user_id = int.Parse(Session["user_id"].ToString());
            
            var topicRepository = new TopicRepository();
            var followTopicRepository = new FollowTopicRepository();

            var topicList = topicRepository.GetAll();

            var userSelectedTopics = followTopicRepository.GetTopicsByUser(user_id);
            List<int> userSelectedTopicIds = new List<int>();

            foreach (var followTopic in userSelectedTopics)
            {
                userSelectedTopicIds.Add(followTopic.TopicID);
            }

            ViewBag.userSelectedTopics = userSelectedTopicIds;

            return View(topicList);
        }

        [HttpPost]
        public ActionResult FollowTopic(FormCollection fc)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            try
            {
                var user_id = int.Parse(Session["user_id"].ToString());

                // feed follow
                var selectedTopics = fc["selectedTopics"].Split(',').ToList();

                FollowTopicRepository topicRepository = new FollowTopicRepository();
                topicRepository.DeleteByUser(user_id);
                topicRepository.AddTopics(user_id, selectedTopics);

                return RedirectToAction("Feed");
            }
            catch
            {
                ViewBag.Message = "Failed to follow the selected topics";
                return View();
            }
        }
    }
}