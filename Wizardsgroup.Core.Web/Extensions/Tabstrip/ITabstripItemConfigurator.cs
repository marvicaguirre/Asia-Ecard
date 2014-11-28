namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITabstripItemConfigurator
    {
        ITabstripItemContentConfigurator TabItemFor(string tabstripTitle);
    }
}