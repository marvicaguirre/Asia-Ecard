using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    internal class ServiceContainer : IServiceContainer
    {
        public Type Key { get; set; }
        public Expression Value { get; set; }
    }
}
