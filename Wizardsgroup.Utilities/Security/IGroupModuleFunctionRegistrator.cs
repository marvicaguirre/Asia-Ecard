namespace Wizardsgroup.Utilities.Security
{
    public interface IGroupModuleFunctionRegistrator
    {
        IModuleRegistrator ForGroup(string groupModuleName);
    }
}