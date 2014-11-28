using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlButtonHelper
    {
        ICreateConfigurator Create(Action<IButtonPropertyConfigurator> configuration);
        IDeleteConfigurator Delete(Action<IButtonPropertyConfigurator> configuration);
        IApprovalConfigurator Approval(Action<IButtonPropertyConfigurator> configuration);
        IToggleConfigurator Toggle(Action<IButtonPropertyConfigurator> configuration);
        IConfirmConfigurator Confirm(Action<IButtonPropertyConfigurator> configuration);
        IModalConfigurator CustomModal(Action<IButtonPropertyConfigurator> configuration);
        ICustomActionConfigurator CustomAction(Action<IButtonPropertyConfigurator> configuration);
        ISelectModalConfiguration SelectionModal(Action<IButtonPropertyConfigurator> configuration);
    }
}