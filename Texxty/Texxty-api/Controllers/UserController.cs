using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TexxtyDataAccess.Models;
using TexxtyDataAccess.Models.Utilities;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api/Users")]
    public class UserController : AccountsController
    {

        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login([FromBody]LoginInfo login)
        {
            var token = AuthenticationUtility.AuthenticateUser(login.Username, login.Password, out int userID);
            if (token != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { token, userID });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage Register([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                string errorMessages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessages);
            }
            else
            {
                bool userNameAvailable = userRepository.CheckUsernameAvailable(user);
                bool emailAvailable = userRepository.CheckEmailAvailable(user);

                if (!userNameAvailable && !emailAvailable)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username not available. Account with Email already exists");
                else if (!userNameAvailable)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username not available.");
                else if (!emailAvailable)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Account with Email already exists");
            }

            try
            {
                user.ActiveStatus = true;
                if (Request.Headers.Authorization != null)
                {
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    var loggedInUser = AuthenticationUtility.VerifyToken(authenticationToken);

                    if (loggedInUser != null)
                    {
                        if (loggedInUser.Role.Equals("admin"))
                        {
                            user.Role = "admin";
                        }
                        else
                        {
                            user.Role = "user";
                        }
                    }
                    else
                    {
                        user.Role = "user";
                    }
                }
                else
                {
                    user.Role = "user";
                }

                user.Token = AuthenticationUtility.GenerateToken();
                userRepository.Insert(user);

                // Return the URI for the new user along with the token
                var resp = Request.CreateResponse(HttpStatusCode.Created, new { user.Token });
                resp.Headers.Location = new Uri(new Uri(Request.RequestUri, ".") + user.UserID.ToString());
                return resp;
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Failed to create new user account.");
            }
        }
    }
}
