using SimpleBlog.Core.BusinessObjects;
using SimpleBlog.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class WidgetViewModel
    {
        public IList<Category> Categories { get; private set; }
        public IList<Tag> Tags { get; private set; }
        public IList<Post> LatestPosts { get; private set; }

        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.GetAllCategories();
            Tags = blogRepository.GetAllTags();
            LatestPosts = blogRepository.Posts(0, 10);
        }
    }
}