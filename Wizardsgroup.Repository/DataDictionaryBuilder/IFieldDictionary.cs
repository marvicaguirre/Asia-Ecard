namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public interface IFieldDictionary
    {
        IModuleDictionary DisplayProperties(string displayText,bool isRequired = false);
    }
}