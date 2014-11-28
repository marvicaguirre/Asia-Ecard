using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ICustomActionConfigurator
    {
        ICustomActionConfigurator Action(Action<IControllerCustomActionConfigurator> configuration);
        MvcHtmlString Render();
    }
}