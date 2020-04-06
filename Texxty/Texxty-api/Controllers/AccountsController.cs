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
        private readonly UserRepository repo = new UserRepository();

        [HttpGet]
        [BearerAuthentication]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var user = repo.GetUserModel(id);

                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Could not find the specified user.");
            }
        }

        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage Register([FromBody]User user)
        {
            try
            {
                user.ActiveStatus = true;
                user.Token = AuthenticationUtility.GenerateToken();

                repo.Insert(user);

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

        public struct LoginInfo
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody]LoginInfo login)
        {
            var token = AuthenticationUtility.AuthenticateUser(login.Username, login.Password);
            if (token != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { token });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
