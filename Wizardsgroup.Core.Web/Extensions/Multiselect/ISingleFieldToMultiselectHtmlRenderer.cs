using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ISingleFieldToMultiselectHtmlRenderer<TModel, TValue>
    {
        MvcHtmlString RenderHtml();
    }
}