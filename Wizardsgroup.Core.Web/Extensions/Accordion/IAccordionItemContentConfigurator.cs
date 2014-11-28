using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IAccordionItemContentConfigurator
    {
        /// <summary>
        /// Expandeds the specified content.
        /// </summary>
        /// <param name="expanded">if set to <c>true</c> [expanded].</param>
        /// <returns>IAccordionItemContentConfigurator.</returns>
        IAccordionItemContentConfigurator Expanded(bool expanded = true);
        /// <summary>
        /// Contents the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>IAccordionItemConfigurator.</returns>
        IAccordionItemConfigurator Content(Func<object, object> value);
        /// <summary>
        /// Contents the specified MVC HTML string.
        /// </summary>
        /// <param name="mvcHtmlString">The MVC HTML string.</param>
        /// <returns>IAccordionItemConfigurator.</returns>
        IAccordionItemConfigurator Content(MvcHtmlString mvcHtmlString);
    }
}