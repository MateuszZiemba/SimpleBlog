using SimpleBlog.Core.BusinessObjects;
using SimpleBlog.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private BlogDBContext db = new BlogDBContext();
        public IList<Post> Posts(int pageNumber, int pageSize)
        {
            var posts = db.Posts.Where(p => p.IsPublished)
                                .OrderByDescending(p => p.PublishedOn)
                                .Skip(pageNumber * pageSize)
                                .Take(pageSize)
                                .Include(p => p.Category)
                                .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return db.Posts.Where(p => postIds.Contains(p.Id))
                           .OrderByDescending(p => p.PublishedOn)
                           .Include(p => p.Tags)
                           .ToList();
        }

        public int PostsCount()
        {
            return (from p in db.Posts
                    where p.IsPublished == true
                    select p).Count();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageSize)
        {
            var posts = db.Posts.Where(p => p.IsPublished && p.Category.UrlSlug.Equals(categorySlug))
                                .OrderByDescending(p => p.PublishedOn)
                                .Skip(pageNumber * pageSize)
                                .Take(pageSize)
                                .Include(p => p.Category)
                                .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return db.Posts.Where(p => postIds.Contains(p.Id))
                           .OrderByDescending(p => p.PublishedOn)
                           .Include(p => p.Tags)
                           .ToList();
        }

        public int PostsCountForCategory(string categorySlug)
        {
            return (from p in db.Posts
                    where p.IsPublished == true && p.Category.UrlSlug.Equals(categorySlug)
                    select p).Count();
        }

        public Category GetCategory(string categorySlug)
        {
            return db.Categories.Where(c => c.UrlSlug.Equals(categorySlug)).FirstOrDefault();
        }
    }
}