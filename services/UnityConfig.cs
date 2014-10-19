using System.Web.Http;
using CampusNext.AzureSearch.Repository;
using CampusNext.Services.BusinessLayer;
using CampusNext.Services.BusinessLayer.AzureSearch;
using Microsoft.Practices.Unity;

namespace CampusNext.Services
{
    public class UnityConfig
    {
        public static void Register()
        {
            var container = new UnityContainer();
            container.RegisterType<IAzureSearchRepository, AzureSearchTextbookRepository>(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}