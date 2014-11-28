using System;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public interface IMasterMenuRegistrator : IModuleItemContainer
    {
        string ModuleName { get; }
        void RegisterMenu(string mainMenu, Action<IMenuRegistrator> register);
    }
}
