using System.Collections.Concurrent;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Service.Lookup
{
    public class LookupSpecificationStore
    {
        public LookupSpecificationStore()
        {
            Store = new ConcurrentDictionary<string, ISpecification<dynamic>>();
        }
        public ConcurrentDictionary<string, ISpecification<dynamic>> Store { get; private set; }
    }
}
