using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Texxty_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Route for search
            config.Routes.MapHttpRoute(
                name: "SearchEntityApi",
                routeTemplate: "api/{controller}/{entity}/{search}"
            );

            config.Routes.MapHttpRoute(
                name: "SearchApi",
                routeTemplate: "api/{controller}/{search}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
