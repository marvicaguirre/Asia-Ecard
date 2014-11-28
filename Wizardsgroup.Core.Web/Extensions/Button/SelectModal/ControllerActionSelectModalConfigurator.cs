using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerActionSelectModalConfigurator : IControllerActionSelectModalConfigurator
    {
        private readonly IControllerActionConfigurator _controllerConfigurator;

        //Compose around IControllerActionConfigurator
        public ControllerActionSelectModalConfigurator(IControllerActionConfigurator controllerConfigurator)
        {
            controllerConfigurator.Guard("IControllerActionConfigurator must not be null.");
            _controllerConfigurator = controllerConfigurator;
        }

        public IControllerActionSelectModalConfigurator Validation(string validationAction)
        {
            _controllerConfigurator.Validation(validationAction);
            return this;
        }

        public IControllerActionSelectModalConfigurator Action(string action)
        {
            _controllerConfigurator.Action(action);
            return this;
        }
    }
}