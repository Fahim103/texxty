using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Slugify;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository;
using Texxty.Repository.Classes;

namespace Texxty.Controllers
{
    public class PostController : Controller 
    {
        private readonly PostRepository postrepo;
        private readonly SlugHelper helper;
        int blogid;

        public PostController()
        {
            postrepo = new PostRepository();
            helper = new SlugHelper();
            blogid = Convert.ToInt32(Session["blogID"]);
        }

        
        // GET: Post
        
        public ActionResult Index()
        { 
             return View(postrepo.GetAllPosts(blogid));
        }
        [HttpGet]
        public ActionResult Create()
        { return View(); }
        [HttpPost]
        public ActionResult Create(Post p)
        { p.ModifiedDate = DateTime.Now;
            p.PublishedDate = DateTime.Now;
            p.BlogID = blogid;
            p.UrlField = helper.GenerateSlug(p.Title.ToString());
            if (p.Draft==false)
            { }
            postrepo.Insert(p);
            return RedirectToAction("Index");




        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(postrepo.Get(id));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(postrepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(Post p)
        {
            

            if (!ModelState.IsValid)
                return View(p);
            p.ModifiedDate = DateTime.Now;

            p.UrlField = helper.GenerateSlug(p.Title.ToString());
                
            postrepo.Update(p);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(postrepo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            postrepo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}