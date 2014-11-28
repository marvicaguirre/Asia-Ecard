using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ISelectModalConfiguration
    {
        ISelectModalConfiguration Modal(Action<IModalProperConfigurator> configuration);
        ISelectModalConfiguration Action(Action<IControllerSelectModalConfigurator> configuration);
        ISelectModalConfiguration SelectionMode(SelectionMode mode);
        MvcHtmlString Render(); 
    }
}