namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public interface IMenuRegistratorSeparator : IMenuRegistrator
    {
        IMenuRegistrator WithSeparator();
    }
}
