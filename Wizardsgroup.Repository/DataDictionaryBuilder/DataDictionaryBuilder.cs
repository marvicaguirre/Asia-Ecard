using System.Collections.Generic;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public class DataDictionaryBuilder : IDataDictionaryBuilder
    {
        public DataDictionaryBuilder()
        {
            Container = new List<DataDictionary>();    
        }

        public List<DataDictionary> Container { get; private set; }

        public IModuleDictionary ForViewModel(string viewModelName)
        {
            viewModelName.Guard("ModelName must not be null or empty.");
            IModuleDictionary moduleDictionary = new ModuleDictionary(this, viewModelName);
            return moduleDictionary;
        }
    }
}