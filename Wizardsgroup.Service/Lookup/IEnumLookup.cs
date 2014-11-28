using System.Collections.Generic;
using Wizardsgroup.Domain.Lookup;

namespace Wizardsgroup.Service.Lookup
{
    public interface IEnumLookup
    {
        IEnumLookupFluentBuilder<TEnum> LookupFluentBuilder<TEnum>();
        IEnumerable<LookupData> GetLookup();
    }
}