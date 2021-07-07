using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Newtonsoft.Json;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Serialization;

namespace OData_Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
              name: "WebApi",
              routeTemplate: "jwt/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
            json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            json.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            json.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
         

            ODataModelBuilder builder = new ODataConventionModelBuilder();

            var devices = builder.EntitySet<Device>("Devices").EntityType;
            devices.Filter().Count().Page().OrderBy();
            devices.Ignore(d => d.Company);
            devices.Ignore(d => d.DeviceStatuses);

            var testers = builder.EntitySet<Tester>("Testers").EntityType;
            testers.Filter().Count().Page().OrderBy().Expand();

			//testers.Ignore(d => d.Companies);
			
			builder.EntitySet<TesterCompany>("NewTester");
			builder.EntitySet<Tester>("UpdateTester");

			var companies = builder.EntitySet<Company>("Companies").EntityType;
            companies.Filter().Count().Page().OrderBy().Expand();
            companies.Ignore(c => c.Devices);
            //companies.Ignore(c => c.Testers);

            var subject = builder.EntitySet<Subject>("Subjects").EntityType;
            subject.Filter().Count().Page().OrderBy();
            subject.Ignore(t => t.Company);

            var tests = builder.EntitySet<Test>("Tests").EntityType;
            tests.Filter().Count().Page().OrderBy();
            tests.Ignore(t => t.Company);
            tests.Ignore(t => t.Tester);

            builder.EntitySet<TestBinary>("TestBinarys");

            var reports = builder.EntitySet<ReportBundle>("Reports").EntityType;
			reports.Filter().Count().Page().OrderBy().Expand();

			var datareports = builder.EntitySet<ReportBundle>("DataReports").EntityType;
			datareports.Filter().Count().Page().OrderBy().Expand();

			config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

            config.Filters.Add(new JwtAuthAttribute());
        }
    }


}
