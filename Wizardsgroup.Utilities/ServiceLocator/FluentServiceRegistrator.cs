using System.Collections.Generic;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    internal class FluentServiceRegistrator : IFluentServiceRegistrator, ICustomServiceContainer
    {
        public List<IServiceContainer> Container { get; private set; }

        public FluentServiceRegistrator()
        {            
            Container = new List<IServiceContainer>();
        }

        public IServiceRegistrator<TService> For<TService>()
        {
            return new ServiceRegistrator<TService>(this);
        }
    }
}