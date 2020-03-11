using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository.Classes;

namespace Texxty.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogRepository repo = new BlogRepository();
        private readonly TopicRepository topicRepo = new TopicRepository();

        [NonAction]
        public bool AuthorizeUser()
        {
            return Session["user_id"] != null;
        }

        // GET: Blog
        public ActionResult Index()
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            //return View(repo.GetAll());
            return View(repo.GetAllBlogsByID(Int32.Parse(Session["user_id"].ToString())));
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            var topicSelectList = new SelectList( topicRepo.GetAll(), "BlogTopicID", "Name", "1");
            ViewBag.topicSelectList = topicSelectList;

            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            try
            {
                SlugHelper slugHelper = new SlugHelper();

                blog.UserID = int.Parse(Session["user_id"].ToString());
                blog.UrlField = slugHelper.GenerateSlug(blog.Title);
                repo.Insert(blog);
                return RedirectToAction("Index");
            }
            catch
            {
                var topicSelectList = new SelectList(topicRepo.GetAll(), "BlogTopicID", "Name", "1");
                ViewBag.topicSelectList = topicSelectList;

                ViewBag.Message = "Failed to create a new blog.";
                return View();
            }
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            var topicSelectList = new SelectList(topicRepo.GetAll(), "BlogTopicID", "Name", "1");
            ViewBag.topicSelectList = topicSelectList;

            return View(repo.Get(id));
        }

        // POST: Blog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Blog blog)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            try
            {
                SlugHelper slugHelper = new SlugHelper();

                Blog b = repo.Get(id);
                b.Title = blog.Title;
                b.Description = blog.Description;
                b.UrlField = slugHelper.GenerateSlug(blog.Title);
                b.TopicID = blog.TopicID;
                b.Private = blog.Private;

                repo.Update(b);
                return RedirectToAction("Index");
            }
            catch
            {
                var topicSelectList = new SelectList(topicRepo.GetAll(), "BlogTopicID", "Name", "1");
                ViewBag.topicSelectList = topicSelectList;

                return View();
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            return View(repo.Get(id));
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login", "Accounts");

            try
            {
                repo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
