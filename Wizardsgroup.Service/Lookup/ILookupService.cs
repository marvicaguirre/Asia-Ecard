using System.Collections.Generic;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Service.Lookup
{
    public interface ILookupService<T> where T : class
    {
        IEnumerable<ILookupValueField> ConvertRecordToLookUp<TType>(IEnumerable<T> listOfRecords) where TType : class;        
    }
}