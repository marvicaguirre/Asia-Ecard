namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public interface IDataDictionaryBuilder : IDataDictionaryCollection
    {
        IModuleDictionary ForViewModel(string viewModelName);
    }
}