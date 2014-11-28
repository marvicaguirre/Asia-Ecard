using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class FuncContentSpecification : IAccordionContentSpecification
    {
        public bool IsSatisifiedBy(AccordionItemContainer container)
        {
            return (container.Content as Func<object, object>) != null;
        }

        public string GetContent(AccordionItemContainer container)
        {
            var content = (Func<object, object>) container.Content;
            return string.Format("<br/>{0}<br/>",content(new object()));
        }
    }
}