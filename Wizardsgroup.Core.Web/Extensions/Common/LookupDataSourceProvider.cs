namespace Wizardsgroup.Core.Web.Extensions
{
    internal class LookupDataSourceProvider : ILookupDataSourceProvider
    {
        private readonly MultiSelectLookupDataSourceProvider _multiSelectLookupDataSourceProvider;

        public LookupDataSourceProvider(MultiSelectLookupDataSourceProvider multiSelectLookupDataSourceProvider)
        {
            _multiSelectLookupDataSourceProvider = multiSelectLookupDataSourceProvider;
        }

        public ILookupDataSource TargetLookup(string targetLookup)
        {
            _multiSelectLookupDataSourceProvider.TargetLookup = targetLookup;
            return _multiSelectLookupDataSourceProvider;
        }
    }
}