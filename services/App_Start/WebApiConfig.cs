using System.Web.Http;
using System.Web.Http.OData.Builder;
using CampusNext.Entity;
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
            builder.EntitySet<FindTutor>("TutorSearch"); 
            return builder.GetEdmModel();    

        }

        public static void Register(HttpConfiguration config)
        {
            // Web API routes    
            config.EnableCors();
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapODataServiceRoute("odata", "odata", GenerateEdmModel());
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
