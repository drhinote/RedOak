using Newtonsoft.Json;
using Roi.Analysis.Api.Controllers;
using Roi.Data;
using Roi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace Roi.Analysis.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			config.EnableCors();
            // Web API configuration and services
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Converters.Add(new ByteArrayConverter());
            json.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
            json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            json.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "HandsetApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

   
        }
    }
}
