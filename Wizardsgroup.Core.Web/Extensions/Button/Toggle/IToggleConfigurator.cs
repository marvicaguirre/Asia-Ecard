using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IToggleConfigurator
    {
        IToggleConfigurator Modal(Action<IModalProperConfigurator> configuration);
        IToggleConfigurator Action(string controller);
        MvcHtmlString Render();
    }
}