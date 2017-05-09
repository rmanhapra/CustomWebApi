using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace SortingAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{id}",
                defaults: new {controller="Sort", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "NamedApi",
                routeTemplate: "api/v2/{id}",
                defaults: new { controller = "SortV2", id = RouteParameter.Optional }
            );

        }
    }
}
