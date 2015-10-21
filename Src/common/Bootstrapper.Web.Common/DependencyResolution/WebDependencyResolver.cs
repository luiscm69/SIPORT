namespace Bootstrapper.Web.Common.DependencyResolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using StructureMap;

    public class WebDependencyResolver : IDependencyResolver
    {
        public WebDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        private readonly IContainer container;

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                object truInstance = this.container.TryGetInstance(serviceType);
                return truInstance;
            }

            object instance = this.container.GetInstance(serviceType);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances<object>()
                .Where(s => s.GetType() == serviceType);
        }
    }
}