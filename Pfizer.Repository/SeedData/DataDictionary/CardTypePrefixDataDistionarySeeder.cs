using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class CardTypePrefixDataDistionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("CardPrefixViewModel")
                    .ForField("ProgramId").DisplayProperties("Program")
                    .ForField("Name").DisplayProperties("Prefix", true);
        }
    }
}
