using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IAccordionHtmlRenderer
    {
        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns>MvcHtmlString.</returns>
        MvcHtmlString Render(); 
    }
}