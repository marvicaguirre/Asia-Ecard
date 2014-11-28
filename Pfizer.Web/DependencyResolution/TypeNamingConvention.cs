using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using WebGrease.Css.Extensions;

namespace Pfizer.Web.DependencyResolution
{
    public class TypeNamingConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            type.GetInterfaces().ForEach(interfaceType => registry.AddType(interfaceType, type));
        }
    }
}