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

            //TopicRepository topicRepo = new TopicRepository();
            //var topicSelectList = new SelectList(topicRepo.GetAll(), "BlogTopicID", "Name", "1");
            //ViewBag.topicSelectList = topicSelectList;

            return View(postRepository.GetAllPublicPosts());
        }
    }
}