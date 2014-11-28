using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Service.Plugin
{
    public abstract class AbstractPluginDescriptor : IPluginDescriptor
    {
        public string Name { get { return Assembly.GetAssembly(GetType()).GetName().Name; } }
        public abstract string Namespace { get;}        
        public string Version { get { return Assembly.GetAssembly(GetType()).GetName().Version.ToString(); } }
        public abstract IUnitOfWork UnitOfWork { get; }
        public string ResolveAreaPrefix()
        {
            return Name;
        }

        public List<IAreaPluginRegistration> AreaPluginRegistrations()
        {
            return ReflectionHelper.Instance
                .GetTypesWithImplementingInterface<IAreaPluginRegistration>(Assembly.GetAssembly(GetType()).ExportedTypes)
                .Select(areaPluginRegistratorType => ReflectionHelper.Instance.CreateInstanceOfType<IAreaPluginRegistration>(areaPluginRegistratorType))
                .ToList();
        }
    }
}