namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ILookupDataSourceProvider
    {
        ILookupDataSource TargetLookup(string targetLookup);
    }
}