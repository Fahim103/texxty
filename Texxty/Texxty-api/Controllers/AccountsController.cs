using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using Texxty_api.Attributes;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.Utilities;
using TexxtyDataAccess.Repository.Classes;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountsController : ApiController
    {
        protected UserRepository userRepository;
        public AccountsController()
        {
            userRepository = new UserRepository();
        }

        public struct LoginInfo
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPut]
        [Route("{user_id}/UpdateInformation")]
        public IHttpActionResult UpdateInformation(int user_id,User user)
        {
            if (string.IsNullOrWhiteSpace(user.FullName))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Fullname cannot be Empty"));

            try
            {
                var dbUser = userRepository.Get(user_id);
                if (dbUser == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "User not found"));

                if (user.Email != null && !dbUser.Email.Equals(user.Email))
                {
                    if (string.IsNullOrWhiteSpace(user.Email))
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Email cannot be Empty"));

                    if (!userRepository.CheckEmailAvailable(user))
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Account with email exists"));

                    dbUser.Email = user.Email;
                }

                dbUser.FullName = user.FullName;
                userRepository.Update(dbUser);
                return Ok(userRepository.GetUserModel(dbUser.UserID));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{user_id}/UpdatePassword")]
        public IHttpActionResult UpdatePassword(int user_id,[FromBody]string currentPassword, [FromBody]string newPassword, [FromBody]string newPasswordConfirm)
        {
            var dbUser = userRepository.Get(user_id);
            if(dbUser == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "User not found"));

            if(!dbUser.Password.Equals(currentPassword))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect password"));

            if(string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(newPasswordConfirm) || !newPassword.Equals(newPasswordConfirm))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "New Password Empty or Doesn't match"));

            try
            {
                dbUser.Password = newPassword;
                userRepository.Update(dbUser);
                return Ok(userRepository.GetUserModel(dbUser.UserID));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
