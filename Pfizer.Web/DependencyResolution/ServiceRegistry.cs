using StructureMap.Configuration.DSL;
using Pfizer.Service;

namespace Pfizer.Web.DependencyResolution
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<UserService>();
                s.Convention<TypeNamingConvention>();                
            });
        }
    }
}