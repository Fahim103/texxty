using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        string PostContent { get; set; }
        bool Draft { get; set; }
        string PublishedDate { get; set; }
        string ModifiedDate { get; set; }
        string URLField { get; set; }



    }
}
