using System;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlAccordionHelper : IHtmlAccordionHelper
    {
        private readonly AccordionConfigurationContainer _configurationContainer = new AccordionConfigurationContainer();

        public HtmlAccordionHelper(HtmlHelper helper,String name)
        {
            helper.Guard("HtmlHelper must not be null.");
            name.Guard("Name must not be null or empty.");
            _configurationContainer.HtmlHelper = helper;
            _configurationContainer.Name = name;
        }

        public IHtmlAccordionHelper ExpandMode(ExpandMode expandMode)
        {
            _configurationContainer.ExpandMode = expandMode;
            return this;
        }

        public IHtmlAccordionHelper Content(Action<IAccordionItemConfigurator> itemRegistration)
        {
            itemRegistration.Guard("ItemRegistration must not be null.");
            _configurationContainer.AccordionItemAction = itemRegistration;
            return this;
        }

        public MvcHtmlString Render()
        {
            return new AccordionHtmlRenderer(_configurationContainer).Render();
        }
    }
}