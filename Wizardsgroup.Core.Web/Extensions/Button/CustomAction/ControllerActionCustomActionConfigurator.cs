namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ControllerActionCustomActionConfigurator : IControllerActionCustomActionConfigurator
    {
        private readonly IControllerActionConfigurator _controllerConfigurator;
        private readonly ButtonConfigurationContainer _container;

        public ControllerActionCustomActionConfigurator(IControllerActionConfigurator controllerConfigurator, ButtonConfigurationContainer container)
        {
            _controllerConfigurator = controllerConfigurator;
            _container = container;
        }

        public IControllerActionCustomActionConfigurator ServerAction(string action)
        {
            _controllerConfigurator.Action(action);
            return this;
        }

        public IControllerActionCustomActionConfigurator ClientAction(string action)
        {
            _container.ClientAction.Action = action;
            return this;
        }

        public IControllerActionCustomActionConfigurator TargetLevel(int? targetLevel)
        {
            _container.ClientAction.TargetLevel = targetLevel;
            return this;
        }
    }
}