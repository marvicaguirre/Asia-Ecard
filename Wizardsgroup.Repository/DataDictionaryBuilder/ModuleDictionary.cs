using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    internal class ModuleDictionary : IModuleDictionary
    {
        private readonly IDataDictionaryCollection _container;
        private readonly string _viewModelName;
        private readonly DataDictionary _dataDictionary = new DataDictionary();

        public ModuleDictionary(IDataDictionaryCollection container, string viewModelName)
        {
            _container = container;
            _viewModelName = viewModelName;
        }

        public IFieldDictionary ForField(string fieldName)
        {
            fieldName.Guard("FieldName must not be empty or null.");
            _dataDictionary.Model = _viewModelName;
            _dataDictionary.FieldName = fieldName;
            IFieldDictionary fieldDictionary = new FieldDictionary(_container, _dataDictionary);
            return fieldDictionary;
        }
    }
}