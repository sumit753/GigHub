using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace GigHub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //to convert the output parameter name of Api into CamelCase
            var Settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter
                            .SerializerSettings;

            Settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            Settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
