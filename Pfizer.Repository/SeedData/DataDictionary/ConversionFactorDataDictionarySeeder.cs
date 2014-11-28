using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class ConversionFactorDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("ConversionFactorViewModel")
                .ForField("ProductName").DisplayProperties("Product")
                .ForField("DosageId").DisplayProperties("Dosage Form")
                .ForField("PfizerCode").DisplayProperties("Pfizer Code", true)
                .ForField("UnitOfMeasureId").DisplayProperties("Unit of Measure", true)
                .ForField("Factor").DisplayProperties("Quantity per Unit of Measure", true);
        }
    }
}
