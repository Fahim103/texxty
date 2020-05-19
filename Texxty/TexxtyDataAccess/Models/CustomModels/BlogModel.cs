using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TexxtyDataAccess.Models.CustomModels
{
    public class BlogModel
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlField { get; set; }
        public bool Private { get; set; }
        public int UserID { get; set; }
        public int TopicID { get; set; }
        public int ViewCount { get; set; }

        public List<string> Resources = new List<string>
            {
                "[GET]      api/Blogs",
                "[GET]      api/Users/{user_id}/Blogs",
                "[GET]      api/Users/{user_id}/Blogs/{blog_id}",
                "[GET]      api/Blogs/{blog_id}",
                "[POST]     api/Blogs",
                "[PUT]      api/Blogs/{blog_id}",
                "[PUT]      api/Blogs/{blog_id}/UpdateViewCount",
                "[DELETE]   api/Blogs/{blog_id}",
            };
    }
}