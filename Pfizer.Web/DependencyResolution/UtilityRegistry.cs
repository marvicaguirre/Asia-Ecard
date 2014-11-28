using StructureMap.Configuration.DSL;
using Wizardsgroup.Utilities.EventAggregator;

namespace Pfizer.Web.DependencyResolution
{
    public class UtilityRegistry : Registry
    {
        public UtilityRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<SimpleEventAggregator>();
                s.Convention<TypeNamingConvention>();
            });
        }
    }
}