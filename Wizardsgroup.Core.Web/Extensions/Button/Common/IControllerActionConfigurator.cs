namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerActionConfigurator
    {        
        IControllerActionConfigurator Validation(string validationAction);
        IControllerActionConfigurator Confirm(string confirmAction);
        IControllerActionConfigurator Action(string action);
    }
}