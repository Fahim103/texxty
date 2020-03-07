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
    public interface IUserMetaData
    {
        int UserID { get; set; }

        [Required(ErrorMessage ="Username is required")]
        string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        string Password { get; set; }

        [Required(ErrorMessage = "Fullname is required")]
        string FullName { get; set; }

        bool ActiveStatus { get; set; }
    }
}
