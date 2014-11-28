using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IComboBoxHtmlRenderer<TModel, TValue>
    {
        MvcHtmlString RenderHtml();
    }
}