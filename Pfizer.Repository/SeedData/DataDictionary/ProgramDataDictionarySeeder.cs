using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class ProgramDataDictionarySeeder: AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("ProgramViewModel")
                   .ForField("CardTypeId").DisplayProperties("Card Type",true)
                   .ForField("Name").DisplayProperties("Program",true)
                   .ForField("Description").DisplayProperties("Description", true)
                   .ForField("VendorCode").DisplayProperties("Vendor Code", true);
        }
    }
}
