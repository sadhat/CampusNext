using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Routing;
using CampusNext.Services.Models;
using Microsoft.Data.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Extensions;

namespace CampusNext.Services
{
    public static class WebApiConfig
    {
        private static IEdmModel GenerateEdmModel()    
 
        {    
            var builder = new ODataConventionModelBuilder();    
            builder.EntitySet<Textbook>("TextbookSearch");    
            return builder.GetEdmModel();    

        }

        public static void Register(HttpConfiguration config)
        {
            // Web API routes    
            config.EnableCors();
            
            config.MapHttpAttributeRoutes();

            ODataRoute mapODataServiceRoute = config.Routes.MapODataServiceRoute("odata", "odata", GenerateEdmModel());
            var setting = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
            config.Formatters.JsonFormatter.SerializerSettings = setting;

            config.Routes.MapHttpRoute(

                name: "DefaultApi",

                routeTemplate: "api/{controller}/{id}",

                defaults: new {id = RouteParameter.Optional}

                );
        }
    }
}
