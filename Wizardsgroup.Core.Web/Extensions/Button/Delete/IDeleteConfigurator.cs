using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IDeleteConfigurator
    {
        IDeleteConfigurator Modal(Action<IModalProperConfigurator> configuration);
        IDeleteConfigurator SelectionMode(SelectionMode mode);
        IDeleteConfigurator Action(Action<IControllerConfigurator> configuration);
        MvcHtmlString Render();
    }
}