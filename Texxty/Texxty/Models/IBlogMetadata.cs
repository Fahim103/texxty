using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texxty.Models
{
    interface IBlogMetadata
    {
        int BlogID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        string Title { get; set; }
        string Description { get; set; }
        string UrlField { get; set; }
        bool Private { get; set; }
    }
}
