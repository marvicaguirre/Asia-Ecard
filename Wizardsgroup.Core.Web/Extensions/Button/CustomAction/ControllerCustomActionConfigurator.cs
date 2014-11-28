using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerCustomActionConfigurator : IControllerCustomActionConfigurator
    {
        private readonly IControllerConfigurator _controllerConfigurator;
        private readonly ButtonConfigurationContainer _container;

        //compose around IControllerConfigurator
        public ControllerCustomActionConfigurator(IControllerConfigurator controllerConfigurator,ButtonConfigurationContainer container)
        {
            controllerConfigurator.Guard("IControllerConfigurator must not be null.");
            _controllerConfigurator = controllerConfigurator;
            _container = container;
        }

        public IControllerActionCustomActionConfigurator Controller(string controller)
        {
            return new ControllerActionCustomActionConfigurator(_controllerConfigurator.Controller(controller), _container); 
        }
    }
}