using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ICreateConfigurator
    {
        ICreateConfigurator Modal(Action<IModalProperConfigurator> configuration);
        ICreateConfigurator Action(string action, string controller);
        ICreateConfigurator Route(object route = null);
        MvcHtmlString Render();
    }
}