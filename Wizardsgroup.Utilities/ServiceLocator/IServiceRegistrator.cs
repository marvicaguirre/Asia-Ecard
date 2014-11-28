using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    public interface IServiceRegistrator<TService>
    {        
        IFluentServiceRegistrator Use(Expression<Func<TService>> service);
    }
}