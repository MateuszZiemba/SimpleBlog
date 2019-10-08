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

        public ViewResult Category(string category, int p = 1)
        {
            string type = "Category";
            var listViewModel = new ListViewModel(blogRepository, type, category, p);
            if (listViewModel.Category == null)
                throw new HttpException(404, "Category not found!");
            
            ViewBag.Title = String.Concat("Latest Posts from category ", listViewModel.Category.Name);
            return View("List", listViewModel);
        }

        public ViewResult Tag(string tag, int p = 1)
        {
            string type = "Tag";
            var listViewModel = new ListViewModel(blogRepository, type, tag, p);
            if (listViewModel.Tag == null)
                throw new HttpException(404, "Tag not found!");

            ViewBag.Title = String.Concat("Latest Posts from tag ", listViewModel.Tag.Name);
            return View("List", listViewModel);
        }

        public ViewResult Search(string s, int p = 1) //todo app.js not working
        {
            string type = "Search";
            var listViewModel = new ListViewModel(blogRepository, type, s, p);

            ViewBag.Title = String.Concat("List of posts found for ", s);
            return View("List", listViewModel);
        }

        public ViewResult Post(int year, int month, string title)
        {
            var post = blogRepository.GetPost(year, month, title);
            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.IsPublished == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");
            return View(post);
        }

        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(blogRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }

    }
}