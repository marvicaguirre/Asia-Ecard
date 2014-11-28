using System;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TabstripItemContentConfigurator : ITabstripItemContentConfigurator
    {
        private readonly ITabstripItemConfigurator _configurator;
        private readonly TabstripItemContainer _container;

        public TabstripItemContentConfigurator(ITabstripItemConfigurator configurator, TabstripItemContainer container)
        {
            configurator.Guard("ITabstripItemConfigurator must not be null.");
            container.Guard("TabstripItemContainer must not be null.");
            _configurator = configurator;
            _container = container;
        }

        public ITabstripItemContentConfigurator Selected(bool selected = true)
        {
            _container.Selected = selected;
            return this;
        }

        public ITabstripItemConfigurator Content(Func<object, object> value)
        {
            value.Guard("Func<object, object> value must not be null.");
            _container.Content = value;
            return _configurator;
        }

        public ITabstripItemConfigurator Content(MvcHtmlString mvcHtmlString)
        {
            mvcHtmlString.Guard("MvcHtmlString must nut be null.");
            _container.Content = mvcHtmlString;
            return _configurator;
        }
    }
}