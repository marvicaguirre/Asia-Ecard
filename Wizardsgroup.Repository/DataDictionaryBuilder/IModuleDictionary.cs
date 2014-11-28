namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public interface IModuleDictionary
    {
        IFieldDictionary ForField(string fieldName);
    }
}