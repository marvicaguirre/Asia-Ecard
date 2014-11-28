using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IModalConfigurator
    {
        IModalConfigurator Modal(Action<IModalProperConfigurator> configuration);
        IModalConfigurator Action(string action, string controller);
        IModalConfigurator Route(object route = null);
        MvcHtmlString Render();
    }
}