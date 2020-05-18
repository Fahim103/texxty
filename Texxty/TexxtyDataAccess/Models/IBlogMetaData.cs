using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models
{
    public interface IBlogMetaData
    {
        int BlogID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        string Description { get; set; }

        
          bool Private { get; set; }
        
         string UrlField { get; set; }        
         int UserID { get; set; }
         int  TopicID { get; set; }
         int ViewCount { get; set; }
    }
}

