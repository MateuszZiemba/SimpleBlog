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
    }
}
