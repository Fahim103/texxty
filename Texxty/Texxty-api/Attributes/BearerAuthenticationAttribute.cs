using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TexxtyDataAccess.Models.Utilities;

namespace Texxty_api.Attributes
{
    public class BearerAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var user = AuthenticationUtility.VerifyToken(authenticationToken);
                if (user != null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.Username), new string[] { user.Role });
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}