using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexxtyDataAccess.Models.CustomModels
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool ActiveStatus { get; set; }
        public string Role { get; set; }

        public List<string> Resources = new List<string>
            {
                "[GET]      /api/Accounts/",
                "[GET]      /api/Accounts/{user_id}/Details",
                "[PUT]      /api/Accounts/{user_id}/UpdateInformation",
                "[PUT]      /api/Accounts/{user_id}/UpdatePassword",
                "[POST]     /api/Users/Register",
                "[POST]     /api/Users/Login",
            };
    }
}
