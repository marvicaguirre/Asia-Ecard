using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlAccordionHelper
    {
        /// <summary>
        /// Expands mode.
        /// </summary>
        /// <param name="expandMode">The expand mode.</param>
        /// <returns>IHtmlAccordionHelper.</returns>
        IHtmlAccordionHelper ExpandMode(ExpandMode expandMode);
        /// <summary>
        /// Contents the specified item registration.
        /// </summary>
        /// <param name="itemRegistration">The item registration.</param>
        /// <returns>IHtmlAccordionHelper.</returns>
        IHtmlAccordionHelper Content(Action<IAccordionItemConfigurator> itemRegistration);
        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns>MvcHtmlString.</returns>
        MvcHtmlString Render();
    }
}