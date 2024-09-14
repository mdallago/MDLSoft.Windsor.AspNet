using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MDLSoft.Windsor.AspNet
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;
        private readonly bool injectActionInvoker;

        public WindsorDependencyResolver(IWindsorContainer container, bool injectActionInvoker = true)
        {
            this.container = container;
            this.injectActionInvoker = injectActionInvoker;
        }

        public object GetService(Type serviceType)
        {
            if (!container.Kernel.HasComponent(serviceType))
                return null;

            var controller = container.Resolve(serviceType) as Controller;

            if (controller == null)
                return container.Resolve(serviceType);

            if (injectActionInvoker)
                controller.ActionInvoker = container.Resolve<IActionInvoker>();

            return controller;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType) ? container.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }
    }
}
