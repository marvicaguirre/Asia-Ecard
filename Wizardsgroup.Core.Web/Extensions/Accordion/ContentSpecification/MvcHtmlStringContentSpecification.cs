using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MvcHtmlStringContentSpecification : IAccordionContentSpecification
    {
        public bool IsSatisifiedBy(AccordionItemContainer container)
        {
            return (container.Content as MvcHtmlString) != null;
        }

        public string GetContent(AccordionItemContainer container)
        {
            var content = (MvcHtmlString)container.Content;
            return string.Format("<br/>{0}<br/>", content.ToHtmlString());
        }
    }
}