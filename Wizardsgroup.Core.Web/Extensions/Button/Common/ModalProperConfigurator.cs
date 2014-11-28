namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ModalProperConfigurator : IModalProperConfigurator
    {
        private readonly ModalProperyContainer _container;

        public ModalProperConfigurator(ModalProperyContainer container)
        {
            _container = container;
        }

        public IModalProperConfigurator Title(string title)
        {
            _container.Title = title;
            return this;
        }

        public IModalProperConfigurator Width(int? width)
        {
            _container.Width = width;
            return this;
        }

        public IModalProperConfigurator Height(int? height)
        {
            _container.Height = height;
            return this;
        }

        public IModalProperConfigurator AutoClose(bool autoClose = true)
        {
            _container.AutoClose = autoClose;
            return this;
        }

        public IModalProperConfigurator ConfirmOnClose(bool confirm = true)
        {
            _container.ConfirmWindow = confirm;
            return this;
        }
    }
}