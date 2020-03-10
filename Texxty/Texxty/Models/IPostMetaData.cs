using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Texxty.Models
{
    /// <summary>
    /// Metadata interface for User table
    /// </summary>
    public interface IPostMetaData
    {
        int PostID { get; set; }
        [Required(ErrorMessage = "Title is required")]
      
        string Title { get; set; }

        [Required(ErrorMessage = "Post Content is required")]
        
        [DisplayName("Content")]
        string PostContent { get; set; }
        
        bool Draft { get; set; }

        [DisplayName("Published on")]
        string PublishedDate { get; set; }
        
        [DisplayName("Last Modified")]
        string ModifiedDate { get; set; }
        
        string UrlField { get; set; }
    }
}
