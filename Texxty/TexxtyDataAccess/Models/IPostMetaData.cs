using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models
{
    interface IPostMetaData
    {
        int PostID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        string Title { get; set; }
        System.DateTime PublishedDate { get; set; }
        System.DateTime ModifiedDate { get; set; }
        string UrlField { get; set; }
        bool Draft { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PostContent is required")]
        string PostContent { get; set; }
        int BlogID { get; set; }
        int ViewCount { get; set; }
    }
}
