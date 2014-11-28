using System;
using System.Linq.Expressions;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    internal class ServiceRegistrator<TService> : IServiceRegistrator<TService>
    {
        private readonly FluentServiceRegistrator _fluentServiceRegistrator;

        public ServiceRegistrator(FluentServiceRegistrator fluentServiceRegistrator)
        {
            fluentServiceRegistrator.Guard("FluentServiceRegistrator must not be null.");
            _fluentServiceRegistrator = fluentServiceRegistrator;
        }

        public IFluentServiceRegistrator Use(Expression<Func<TService>> service)
        {
            service.Guard("Expression must not be null");
            var visitor = new ServiceRegistratorVisitor(service);
            visitor.Visit<TService>(_fluentServiceRegistrator);
            return _fluentServiceRegistrator;
        }
    }
}
