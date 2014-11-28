using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class UserGroupDataDictionary : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("UserGroupViewModel")
                .ForField("Name").DisplayProperties("Name", true)
                .ForField("Description").DisplayProperties("Description", true);
        }
    }
}