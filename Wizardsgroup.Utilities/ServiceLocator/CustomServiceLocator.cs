using System;
using System.Linq.Expressions;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    public class CustomServiceLocator : ICustomServiceLocator
    {        
        readonly FluentServiceRegistrator _fluentRegistrator = new FluentServiceRegistrator();

        #region Constructor
        public CustomServiceLocator(Action<IFluentServiceRegistrator> registrator)
        {                       
            registrator.Guard("Action<IFluentServiceRegistrator> must not be null.");            
            registrator(_fluentRegistrator);
            CheckAndThrowEmptyRegistry();
        }
        
        #endregion

        #region Public Functions/Methods
        public TService Resolve<TService>() where TService : class
        {
            var type = GetType<TService>();
            if (!HasRequiredService<TService>())
            {
                throw new ArgumentException(string.Format("{0} is not registered. Register service in ICustomServiceLocator using IFluentServiceRegistrator.",type));
            }
            var serviceExpression = (Expression<Func<TService>>)_fluentRegistrator.Container.Find(o => o.Key == type).Value;
            return serviceExpression.Compile().Invoke();
        }
        public bool HasRequiredService<TService>() where TService : class
        {
            var result = _fluentRegistrator.Container.Find(o => o.Key == GetType<TService>());
            return result != null;
        }
        #endregion        

        #region Private Functions/Method
        private void CheckAndThrowEmptyRegistry()
        {
            if (_fluentRegistrator.Container.Count == 0)
                throw new ArgumentException("No service registered found. Must register at least one service type.");
        }
        private Type GetType<TService>()
        {
            return typeof(TService);
        }
        #endregion
    }
}