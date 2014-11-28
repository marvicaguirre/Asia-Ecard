using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITabstripHtmlRenderer
    {
        MvcHtmlString Render();
    }
}