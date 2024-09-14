using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;

namespace MDLSoft.Windsor.AspNet
{
    public static class WindsorExtensions
    {
        public static void InjectProperties(this IKernel kernel, object target)
        {
            var type = target.GetType();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanWrite && kernel.HasComponent(property.PropertyType))
                {
                    var value = kernel.Resolve(property.PropertyType);
                    try
                    {
                        property.SetValue(target, value, null);
                    }
                    catch (Exception ex)
                    {
                        var message =
                            $"Error setting property {property.Name} on type {type.FullName}, See inner exception for more information.";
                        throw new ComponentActivatorException(message,ex,null);//TODO: Revisar el parámetro null
                    }
                }
            }
        }

        public static BasedOnDescriptor FirstInterfaceOnType(this ServiceDescriptor serviceDescriptor)
        {
            return serviceDescriptor.Select((type, baseType) =>
            {
                var interfaces = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).ToList();
                return !interfaces.Any() ? null : new[] { interfaces.First() };
            });
        }
    }
}
