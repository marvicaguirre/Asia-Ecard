namespace Wizardsgroup.Core.Web.Helpers.ModuleProvider
{
    public interface IModuleItem
    {
        string ModuleName { get; set; }
        string FunctionName { get; set; }
        string ModuleItemText { get; set; }
        string ControllerAction { get; set; }
        string ControllerName { get; set; }
        string ControllerArea { get; set; }
        string TabCaption { get; set; }
        string ParameterKey { get; set; }
        string MenuTitle { get; set; }
    }
}