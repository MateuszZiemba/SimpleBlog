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
    }
}

//var GenreLst = new List<string>();

//var GenreQry = from d in db.Movies
//               orderby d.Genre
//               select d.Genre;

//GenreLst.AddRange(GenreQry.Distinct());
//            ViewBag.movieGenre = new SelectList(GenreLst);

//var movies = from m in db.Movies
//             select m;

//            if (!String.IsNullOrEmpty(searchString))
//            {
//                movies = movies.Where(s => s.Title.Contains(searchString));
//            }

//            if (!string.IsNullOrEmpty(movieGenre))
//            {
//                movies = movies.Where(x => x.Genre == movieGenre);
//            }

//            return View(movies);