using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class SalesRetailPriceDataDictionary : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("SalesRetailPriceViewModel")
                .ForField("ProductName").DisplayProperties("Product")
                .ForField("DosageForm").DisplayProperties("Dosage Form")
                .ForField("PriceTypeName").DisplayProperties("PriceTypeName")
                .ForField("From").DisplayProperties("From", true)
                .ForField("To").DisplayProperties("To")
                .ForField("Price").DisplayProperties("Price", true);
        }
    }
}
