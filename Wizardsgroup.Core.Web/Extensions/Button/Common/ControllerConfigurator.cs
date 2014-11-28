using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerConfigurator : IControllerConfigurator
    {
        private readonly ControllerPropertyContainer _container;

        public ControllerConfigurator(ControllerPropertyContainer container)
        {
            _container = container;
        }

        public IControllerActionConfigurator Controller(string controller)
        {
            controller.Guard("Controller must not be null or empty.");
            _container.Controller = controller;
            return new ControllerActionConfigurator(_container);
        }
    }
}