using System.Web.Http;
using CampusNext.Services.BusinessLayer;
using Microsoft.Practices.Unity;

namespace CampusNext.Services
{
    public class UnityConfig
    {
        public static void Register()
        {
            var container = new UnityContainer();
            container.RegisterType<ITextbookRepository, TextbookRepository>(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}