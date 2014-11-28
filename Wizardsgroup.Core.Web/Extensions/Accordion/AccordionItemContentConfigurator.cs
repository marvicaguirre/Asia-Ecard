using System;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class AccordionItemContentConfigurator : IAccordionItemContentConfigurator
    {
        private readonly IAccordionItemConfigurator _configurator;
        private readonly AccordionItemContainer _accordionItemContainer;

        public AccordionItemContentConfigurator(IAccordionItemConfigurator configurator, AccordionItemContainer accordionItemContainer)
        {
            configurator.Guard("AccordionItemConfigurator must not be null.");
            accordionItemContainer.Guard("AccordionItemContainer must not be null.");
            _configurator = configurator;
            _accordionItemContainer = accordionItemContainer;
        }

        public IAccordionItemContentConfigurator Expanded(bool expanded = true)
        {
            _accordionItemContainer.Expanded = expanded;
            return this;
        }

        public IAccordionItemConfigurator Content(Func<object, object> value)
        {
            _accordionItemContainer.Content = value;
            return _configurator;
        }

        public IAccordionItemConfigurator Content(MvcHtmlString mvcHtmlString)
        {
            _accordionItemContainer.Content = mvcHtmlString;
            return _configurator;
        }
    }
}