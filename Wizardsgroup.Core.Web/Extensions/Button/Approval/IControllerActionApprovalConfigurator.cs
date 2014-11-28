namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerActionApprovalConfigurator
    {
        IControllerActionApprovalConfigurator Validation(string validationAction);
        IControllerActionApprovalConfigurator Action(string action); 
    }
}