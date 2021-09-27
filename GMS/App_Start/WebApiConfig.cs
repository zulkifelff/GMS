using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GMS
{
    public static class WebApiConfig
    {
        
        public static void Register(HttpConfiguration config)
        {
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:4200/", "*", "GET,POST");
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "GET,POST");
            //config.EnableCors(cors);
            //config.Filters.Add(new BasicAuthenticationAttribute());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
