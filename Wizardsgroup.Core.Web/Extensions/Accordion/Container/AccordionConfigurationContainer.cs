using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class AccordionConfigurationContainer
    {
        public AccordionConfigurationContainer()
        {
            Contents = new List<AccordionItemContainer>();
        }
        public string Name { get; set; }
        public ExpandMode ExpandMode { get; set; }
        public HtmlHelper HtmlHelper { get; set; }
        public List<AccordionItemContainer> Contents { get; set; }
        public Action<IAccordionItemConfigurator> AccordionItemAction { get; set; }
    }
}