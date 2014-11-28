namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IControllerConfigurator
    {
        IControllerActionConfigurator Controller(string controller);
    }
}