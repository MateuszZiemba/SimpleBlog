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

        public IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageSize)
        {
            var posts = db.Posts.Where(p => p.IsPublished && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
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

        public int PostsCountForTag(string tagSlug)
        {
            return db.Tags.Where(t => t.UrlSlug.Equals(tagSlug)).SelectMany(p => p.Posts).Count();
        }

        public Tag GetTag(string tagSlug)
        {
            return db.Tags.Where(t => t.UrlSlug.Equals(tagSlug)).FirstOrDefault();
        }

        public IList<Post> PostsForSearch(string search, int pageNumber, int pageSize)
        {
            var posts = db.Posts.Where(p => p.IsPublished && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
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

        public int PostsCountForSearch(string search)
        {
            return (from p in db.Posts
                    where p.IsPublished == true && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search)))
                    select p).Count();
        }

        public Post GetPost(int year, int month, string postSlug)
        {
            return db.Posts.Where(p => p.UrlSlug.Equals(postSlug) 
                               && p.PublishedOn.Year.Equals(year) 
                               && p.PublishedOn.Month.Equals(month))
                               .Include(c => c.Category)
                               .FirstOrDefault();
        }
        public IList<Category> GetAllCategories()
        {
            return db.Categories.OrderBy(c => c.Name).ToList();
        }

        public IList<Tag> GetAllTags()
        {
            return db.Tags.OrderBy(t => t.Name).ToList();
        }
    }
}