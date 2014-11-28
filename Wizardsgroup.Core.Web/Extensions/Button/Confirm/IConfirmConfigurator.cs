using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IConfirmConfigurator
    {
        IConfirmConfigurator Modal(Action<IModalProperConfigurator> configuration);
        IConfirmConfigurator SelectionMode(SelectionMode mode);
        IConfirmConfigurator Action(Action<IControllerConfigurator> configuration);
        MvcHtmlString Render(); 
    }
}