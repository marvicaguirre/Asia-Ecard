using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class SystemMessageDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("SystemMessageViewModel")
                   .ForField("Code").DisplayProperties("Code")
                   .ForField("Message").DisplayProperties("Message", true);
        }
    }
}
