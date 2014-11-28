using System;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public interface IMenuRegistrator
    {
        //ModuleName = GetRegisteredModuleFromFunctionName("ViewUserGroup"),
        //FunctionName = "ViewUserGroup",
        IMenuRegistratorSeparator ForModuleFunction(string functionName, Action<IControllerRegistrator> register);
        //IMenuRegistratorSeparator ForSubMenu(string functionName, Action<IControllerRegistrator> register);
    }
}
