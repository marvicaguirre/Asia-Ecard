using System.Collections.Generic;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Service.Plugin
{
    public interface IPluginDescriptor
    {
        //TODO expand more
        string Name { get; }        
        string Namespace { get; }
        string Version { get; }
        IUnitOfWork UnitOfWork { get; }
        string ResolveAreaPrefix();
        List<IAreaPluginRegistration> AreaPluginRegistrations();

    }
}