using SimpleBlog.Core.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Repositories.Contracts
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNumber, int pageSize);
        int PostsCount();
        IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageSize);
        int PostsCountForCategory(string categorySlug);
        Category GetCategory(string categorySlug);
        IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageSize);
        int PostsCountForTag(string tagSlug);
        Tag GetTag(string tagSlug);
        IList<Post> PostsForSearch(string search, int pageNumber, int pageSize);
        int PostsCountForSearch(string search);
        Post GetPost(int year, int month, string postSlug);
        IList<Category> GetAllCategories();
        IList<Tag> GetAllTags();
    }
}
