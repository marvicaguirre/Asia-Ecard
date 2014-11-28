namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public interface IModuleDisplayRegistrator
    {
        //ModuleItemText = "User Groups and Functions",
        //TabCaption = "User Groups and Functions"
        IControllerRegistrator DisplayProperties(string moduleItemText, string tabCaption = "");
    }
}
