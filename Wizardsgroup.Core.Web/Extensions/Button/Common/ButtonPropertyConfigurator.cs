namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ButtonPropertyConfigurator : IButtonPropertyConfigurator
    {
        private readonly ButtonPropertyContainer _container;

        public ButtonPropertyConfigurator(ButtonPropertyContainer container)
        {
            _container = container;
        }

        public IButtonPropertyConfigurator Width(int? width)
        {
            _container.Width = width ?? 100;
            return this;
        }

        public IButtonPropertyConfigurator GridName(string gridName)
        {
            _container.GridName = gridName;
            return this;
        }

        public IButtonPropertyConfigurator ParentKey(string parentKey = "")
        {
            _container.ParentKey = parentKey;
            return this;
        }
    }
}