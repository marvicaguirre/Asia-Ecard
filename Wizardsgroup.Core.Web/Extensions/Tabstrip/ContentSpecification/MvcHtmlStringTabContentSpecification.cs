using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MvcHtmlStringTabContentSpecification : ITabContentSpecification
    {
        public bool IsSatisifiedBy(TabstripItemContainer container)
        {
            return (container.Content as MvcHtmlString) != null;
        }

        public string GetContent(TabstripItemContainer container)
        {
            var content = (MvcHtmlString)container.Content;
            return string.Format("<br/>{0}<br/>", content.ToHtmlString());
        }
    }
}