using System.Collections.Generic;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public interface IDataDictionaryCollection
    {
        List<DataDictionary> Container { get; }
    }
}