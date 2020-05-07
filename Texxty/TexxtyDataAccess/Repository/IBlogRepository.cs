using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        void CountViews(int blogid);
        bool CheckBlogPrivate(int id);
        List<Blog> GetAllBlogsByID(int id);
        List<BlogModel> GetBlogModelList();
        BlogModel GetBlogModel(int id);
    }
}
