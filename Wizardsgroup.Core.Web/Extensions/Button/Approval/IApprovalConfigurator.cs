using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IApprovalConfigurator
    {
        IApprovalConfigurator Modal(Action<IModalProperConfigurator> configuration);
        IApprovalConfigurator SelectionMode(SelectionMode mode);
        IApprovalConfigurator Action(Action<IControllerApprovalConfigurator> configuration);
        MvcHtmlString Render();
    }
}