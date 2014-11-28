namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerActionSelectModalConfigurator
    {
        IControllerActionSelectModalConfigurator Validation(string validationAction);
        IControllerActionSelectModalConfigurator Action(string action);
    }
}