using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models.CustomModels
{
    public class BlogTopicsModel
    {
        public int BlogTopicID { get; set; }
        public string Name { get; set; }
        public List<string> Resources = new List<string>
            {
                "[GET]      api/Topics",
                "[POST]     api/Topics",
                "[GET]      api/Topics/{topic_id}",
                "[PUT]      api/Topics/{topic_id}",
                "[DELETE]   api/Topics/{topic_id}",
            };
    }
}
