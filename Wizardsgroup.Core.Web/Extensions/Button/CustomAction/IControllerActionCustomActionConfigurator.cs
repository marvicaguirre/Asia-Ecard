namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerActionCustomActionConfigurator
    {
        IControllerActionCustomActionConfigurator ServerAction(string action);
        IControllerActionCustomActionConfigurator ClientAction(string action);
        IControllerActionCustomActionConfigurator TargetLevel(int? targetLevel);
    }
}