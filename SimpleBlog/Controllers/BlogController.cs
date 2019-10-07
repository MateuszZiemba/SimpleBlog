using SimpleBlog.Core.Repositories.Contracts;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult Posts(int p = 1)
        {
            var listViewModel = new ListViewModel(blogRepository, p);
            ViewBag.Title = "Latest Posts";
            return View("List", listViewModel);
        }
    }
}