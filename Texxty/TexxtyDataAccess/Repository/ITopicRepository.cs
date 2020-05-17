using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.CustomModels;

namespace TexxtyDataAccess.Repository
{
    public interface ITopicRepository : IRepository<BlogTopic>
    {
        List<BlogTopicsModel> GetTopicsModelsList();
        BlogTopicsModel GetTopicsModelByID(int topic_id);
    }
}
