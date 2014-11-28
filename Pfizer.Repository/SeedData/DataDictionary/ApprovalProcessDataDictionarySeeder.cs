using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class ApprovalProcessDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("ApprovalProcessViewModel")
                   .ForField("Name").DisplayProperties("Process")
                   .ForField("StrictApproval").DisplayProperties("Strict Approval");
        }
    }
}
