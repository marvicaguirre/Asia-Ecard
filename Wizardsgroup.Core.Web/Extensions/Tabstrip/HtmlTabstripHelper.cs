using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlTabstripHelper : IHtmlTabstripHelper
    {
        private readonly TabstripItemConfigurationContainer _configurationContainer = new TabstripItemConfigurationContainer();

        public HtmlTabstripHelper(HtmlHelper helper, String name)
        {
            helper.Guard("HtmlHelper must not be null.");
            name.Guard("Name must not be null or empty.");
            _configurationContainer.HtmlHelper = helper;
            _configurationContainer.Name = name;
        }

        public IHtmlTabstripHelper HtmlAttributes(object attributes)
        {
            attributes.Guard("Attributes must not be null");
            _configurationContainer.HtmlAttributes = attributes;
            return this;
        }

        public IHtmlTabstripHelper HtmlAttributes(IDictionary<string, object> attributes)
        {
            attributes.Guard("Attributes must not be null");
            _configurationContainer.HtmlAttributes = attributes;
            return this;
        }

        public ITabstripHtmlRenderer Content(Action<ITabstripItemConfigurator> itemRegistration)
        {
            itemRegistration.Guard("ItemRegistration must not be null.");
            _configurationContainer.TabstripItemAction = itemRegistration;
            return new TabstripHtmlRenderer(_configurationContainer);
        }
    }
}