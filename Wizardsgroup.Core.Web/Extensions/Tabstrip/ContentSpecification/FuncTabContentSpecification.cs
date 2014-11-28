using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class FuncTabContentSpecification : ITabContentSpecification
    {
        public bool IsSatisifiedBy(TabstripItemContainer container)
        {
            return (container.Content as Func<object, object>) != null;
        }

        public string GetContent(TabstripItemContainer container)
        {
            var content = (Func<object, object>)container.Content;
            return string.Format("<br/>{0}<br/>", content(new object()));
        }
    }
}