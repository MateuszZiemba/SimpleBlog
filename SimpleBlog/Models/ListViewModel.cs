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
        private int PageSize { get { return 10; } }
        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
        public Category Category { get; private set; }
        public Tag Tag { get; private set; }
        public string Search { get; private set; }

        public ListViewModel(IBlogRepository blogRepository, int pageNumber)
        {
            Posts = blogRepository.Posts(pageNumber - 1, PageSize);
            TotalPosts = blogRepository.PostsCount();
        }

        public ListViewModel(IBlogRepository blogRepository, string type, string text, int pageNumber)
        {
            switch (type)
            {
                case "Tag":
                    Posts = blogRepository.PostsForTag(text, pageNumber - 1, PageSize);
                    TotalPosts = blogRepository.PostsCountForTag(text);
                    Tag = blogRepository.GetTag(text);
                    break;
                case "Category":
                    Posts = blogRepository.PostsForCategory(text, pageNumber - 1, PageSize);
                    TotalPosts = blogRepository.PostsCountForCategory(text);
                    Category = blogRepository.GetCategory(text);
                    break;
                case "Search":
                    Posts = blogRepository.PostsForSearch(text, pageNumber - 1, PageSize);
                    TotalPosts = blogRepository.PostsCountForSearch(text);
                    Search = text;
                    break;
                default:
                    break;
            }
        }
    }
}