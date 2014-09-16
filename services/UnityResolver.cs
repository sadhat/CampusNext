using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace CampusNext.Services
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer Container;

        public UnityResolver(IUnityContainer container)
        {
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (Container.IsRegistered(serviceType)) return Container.Resolve(serviceType);
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return null;
            }
            return Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            IUnityContainer child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}