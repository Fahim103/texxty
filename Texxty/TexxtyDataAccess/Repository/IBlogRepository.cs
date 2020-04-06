using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexxtyDataAccess.Models;

namespace TexxtyDataAccess.Repository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        void CountViews(int blogid);
        bool CheckBlogPrivate(int id);
        List<Blog> GetAllBlogsByID(int id);
    }
}
