using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerActionApprovalConfigurator : IControllerActionApprovalConfigurator
    {
        private readonly IControllerActionConfigurator _controllerConfigurator;

        //Compose around IControllerActionConfigurator
        public ControllerActionApprovalConfigurator(IControllerActionConfigurator controllerConfigurator)
        {
            controllerConfigurator.Guard("IControllerActionConfigurator must not be null.");
            _controllerConfigurator = controllerConfigurator;
        }

        public IControllerActionApprovalConfigurator Validation(string validationAction)
        {
            _controllerConfigurator.Validation(validationAction);
            return this;
        }

        public IControllerActionApprovalConfigurator Action(string action)
        {
            _controllerConfigurator.Action(action);
            return this;
        }
    }
}