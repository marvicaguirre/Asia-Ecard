namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public interface IControllerRegistrator
    {
        //ControllerArea = "Security",
        //ControllerName = "UserGroup",
        //ControllerAction = "Index",
        IModuleDisplayRegistrator ControllerProperties(string controllerArea, string controllerName, string controllerAction);
    }
}
