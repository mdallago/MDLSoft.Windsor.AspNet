using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor.Installer;

namespace MDLSoft.Windsor.AspNet
{
    public class WindsorBootstrap : InstallerFactory
    {
        public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
        {
            var retval = installerTypes.OrderBy(GetPriority);
            return retval;
        }

        private int GetPriority(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(InstallerPriorityAttribute), false).FirstOrDefault() as InstallerPriorityAttribute;
            return attribute?.Priority ?? InstallerPriorityAttribute.DEFAULT_PRIORITY;
        }
    }
}