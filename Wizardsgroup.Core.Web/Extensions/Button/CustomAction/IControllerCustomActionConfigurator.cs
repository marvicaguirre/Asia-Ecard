namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerCustomActionConfigurator
    {
        IControllerActionCustomActionConfigurator Controller(string controller);
    }
}