using System;

namespace MDLSoft.Windsor.AspNet
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InstallerPriorityAttribute : Attribute
    {
        public const int DEFAULT_PRIORITY = 100;

        public int Priority { get; }

        public InstallerPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}