namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerApprovalConfigurator
    {
        IControllerActionApprovalConfigurator Controller(string controller);
    }
}