using Castle.Windsor;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MDLSoft.Windsor.AspNet
{
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        readonly IWindsorContainer container;

        public WindsorActionInvoker(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(
                ControllerContext controllerContext,
                IList<IActionFilter> filters,
                ActionDescriptor actionDescriptor,
                IDictionary<string, object> parameters)
        {
            foreach (IActionFilter actionFilter in filters)
            {
                container.Kernel.InjectProperties(actionFilter);
            }
            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }
    }
}
