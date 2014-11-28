using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerActionConfigurator : IControllerActionConfigurator
    {
        private readonly ControllerPropertyContainer _container;

        public ControllerActionConfigurator(ControllerPropertyContainer container)
        {
            _container = container;
        }

        public IControllerActionConfigurator Validation(string validationAction)
        {
            validationAction.Guard("ValidationAction must not be null or empty.");
            _container.ValidationAction = validationAction;
            return this;
        }

        public IControllerActionConfigurator Confirm(string confirmAction)
        {
            confirmAction.Guard("ValidationAction must not be null or empty.");
            _container.ConfirmAction = confirmAction;
            return this;
        }

        public IControllerActionConfigurator Action(string action)
        {
            action.Guard("ValidationAction must not be null or empty.");
            _container.TargetAction = action;
            return this;
        }
    }
}