using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models
{
    public interface IBlogTopicMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Topic Name is required")]
        string Name { get; set; }
    }
}
