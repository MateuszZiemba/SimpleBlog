using SimpleBlog.Core.BusinessObjects;
using SimpleBlog.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class ListViewModel
    {
        public ListViewModel(IBlogRepository blogRepository, int pageNumber)
        {
            Posts = blogRepository.Posts(pageNumber - 1, 10);
            TotalPosts = blogRepository.PostsCount();
        }
        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
    }
}