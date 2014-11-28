using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerApprovalConfigurator : IControllerApprovalConfigurator
    {
        private readonly IControllerConfigurator _controllerConfigurator;

        //compose around IControllerConfigurator
        public ControllerApprovalConfigurator(IControllerConfigurator controllerConfigurator)
        {
            controllerConfigurator.Guard("IControllerConfigurator must not be null.");
            _controllerConfigurator = controllerConfigurator;
        }

        public IControllerActionApprovalConfigurator Controller(string controller)
        {            
            return new ControllerActionApprovalConfigurator(_controllerConfigurator.Controller(controller));
        }
    }
}