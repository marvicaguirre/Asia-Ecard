namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IButtonPropertyConfigurator
    {        
        IButtonPropertyConfigurator Width(int? width);
        IButtonPropertyConfigurator GridName(string gridName);
        IButtonPropertyConfigurator ParentKey(string parentKeyName = "");
    }
}