using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models
{
    public interface IUserMetaData
    {
        int UserID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(length: 8, ErrorMessage = "Password must be 8 character long")]
        string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fullname is required")]
        [Display(Name = "Full Name")]
        string FullName { get; set; }

        bool ActiveStatus { get; set; }
    }
}
