using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Web.Http;
using Texxty_api.Attributes;
using TexxtyDataAccess.Models.Utilities;

namespace Texxty_api.Controllers
{
    [RoutePrefix("api")]
    public class AdminController : AccountsController
    {
        private bool IsUserAdmin()
        {
            return Thread.CurrentPrincipal.IsInRole("admin") ? true : false;
        }


        [HttpPost]
        [Route("Admin/Login")]
        public IHttpActionResult Login([FromBody]LoginInfo login)
        {
            var token = AuthenticationUtility.AuthenticateUser(login.Username, login.Password, out string role, out int userID);
            if (token != null)
            {
                if (role.Equals("admin"))
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { token, userID }));
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden));
                }
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
        }
    }
}
