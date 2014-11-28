using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    internal class FieldDictionary : IFieldDictionary
    {
        private readonly IDataDictionaryCollection _container;
        private readonly DataDictionary _dataDictionary;

        public FieldDictionary(IDataDictionaryCollection container, DataDictionary dataDictionary)
        {
            _container = container;
            _dataDictionary = dataDictionary;
        }

        public IModuleDictionary DisplayProperties(string displayText,bool isRequired = false)
        {
            displayText.Guard("DisplayText must not be empty or null.");
            _dataDictionary.FieldDisplayText = displayText;
            _dataDictionary.IsRequired = isRequired;
            _container.Container.Add(_dataDictionary);
            IModuleDictionary moduleDictionary = new ModuleDictionary(_container, _dataDictionary.Model);
            return moduleDictionary;
        }
    }
}