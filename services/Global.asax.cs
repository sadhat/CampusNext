using System.Web.Http;

namespace CampusNext.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //DocumentDbSeeder.Seed();
        }
    }
}
