using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerSelectModalConfigurator : IControllerSelectModalConfigurator
    {
        private readonly IControllerConfigurator _controllerConfigurator;

        //compose around IControllerConfigurator
        public ControllerSelectModalConfigurator(IControllerConfigurator controllerConfigurator)
        {
            controllerConfigurator.Guard("IControllerConfigurator must not be null.");
            _controllerConfigurator = controllerConfigurator;
        }

        public IControllerActionSelectModalConfigurator Controller(string controller)
        {
            return new ControllerActionSelectModalConfigurator(_controllerConfigurator.Controller(controller));
        }
    }
}