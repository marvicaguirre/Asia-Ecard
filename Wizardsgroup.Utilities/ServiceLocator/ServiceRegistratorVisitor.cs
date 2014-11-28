using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.ServiceLocator
{
    internal class ServiceRegistratorVisitor
    {
        #region Member
        private readonly Expression _expression;
        private List<Action<Type, ICustomServiceContainer>> ActionsToInvoke { get; set; }
        #endregion

        #region Constructor
        public ServiceRegistratorVisitor(Expression expression)
        {
            expression.Guard("Expresion must not be null.");
            _expression = expression;
            ActionsToInvoke = new List<Action<Type, ICustomServiceContainer>>
                {
                    UpdateRegisteredType,
                    RegisterNewServiceType
                };
        } 
        #endregion

        #region Public Functions
        public void Visit<TService>(ICustomServiceContainer fluentServiceRegistrator)
        {
            fluentServiceRegistrator.Guard("FluentServiceRegistrator must not be null.");            
            ActionsToInvoke.ForEach(action=>action(typeof(TService),fluentServiceRegistrator));
        } 
        #endregion

        #region Private Functions/Methods
        void UpdateRegisteredType(Type type, ICustomServiceContainer fluentServiceRegistrator)
        {
            var isNotRegistered = fluentServiceRegistrator.Container.All(o => o.Key != type);
            if (isNotRegistered)
                return;
            
                fluentServiceRegistrator.Container.Find(o => o.Key == type).Value = _expression;
        }

        void RegisterNewServiceType(Type type, ICustomServiceContainer fluentServiceRegistrator)
        {
            var isRegistered = fluentServiceRegistrator.Container.Any(o => o.Key == type);
            if (isRegistered)
                return;

            fluentServiceRegistrator.Container.Add(new ServiceContainer
            {
                Key = type,
                Value = _expression
            });
        } 
        #endregion
    }
}