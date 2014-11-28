using System;
using System.Collections.Generic;

namespace Wizardsgroup.Domain.Interfaces
{
    public interface ILookupFunction
    {
        IEnumerable<ILookupValueField> GetRecordsForLookup();
        IEnumerable<ILookupValueField> GetRecordsForCascade(int id);
        object Specification { get; set; }
        string TextFilter {get;set;}        
    }
}