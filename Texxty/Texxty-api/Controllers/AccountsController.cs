using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using Texxty_api.Attributes;
using Texxty_api.Models;
using Texxty_api.Models.Utilities;
using Texxty_api.Repository.Classes;

namespace Texxty_api.Controllers
{
    public class AccountsController : ApiController
    {
        [BasicAuthentication]
        // TEST METHOD 
        public IHttpActionResult GetUserInfo()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            if (username != null)
                return Ok(username + "-> GetUserInfo");
            else
                return Ok("ERROR");
        }

        [HttpPost]
        public IHttpActionResult Login()
        {
            if (Request.Headers.Authorization == null)
                return StatusCode(HttpStatusCode.Unauthorized);

            // Get authenticationToken from the Request Header
            string authenticationToken = Request.Headers.Authorization.Parameter;

            if (AuthenticationUtility.AuthenticateUser(authenticationToken))
            {
                return Ok("Login Successful");
            }
            else
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
        }
    }
}
