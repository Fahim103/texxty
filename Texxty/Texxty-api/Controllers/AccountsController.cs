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

        [HttpGet]
        [BearerAuthentication]
        [Route("{user_id}/Details")]
        public IHttpActionResult GetUserByID(int user_id)
        {
            try
            {
                var user = userRepository.GetUserModel(user_id);

                if (user == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, user));
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not find the specified user."));
            }
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

        public struct PasswordChangeInfo
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public string NewPasswordConfirm { get; set; }

        }

        [HttpPut]
        [Route("{user_id}/UpdatePassword")]
        public IHttpActionResult UpdatePassword(int user_id,[FromBody]PasswordChangeInfo passwordChangeInfo)
        {
            var dbUser = userRepository.Get(user_id);
            if(dbUser == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "User not found"));

            if(!dbUser.Password.Equals(passwordChangeInfo.CurrentPassword))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect password"));

            if(string.IsNullOrWhiteSpace(passwordChangeInfo.NewPassword) || string.IsNullOrWhiteSpace(passwordChangeInfo.NewPasswordConfirm) || !passwordChangeInfo.NewPassword.Equals(passwordChangeInfo.NewPasswordConfirm))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "New Password Empty or Doesn't match"));

            try
            {
                dbUser.Password = passwordChangeInfo.NewPassword;
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
