using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class ApprovalProcessLevelDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("ApprovalProcessLevelViewModel")
                   .ForField("ApprovalProcessName").DisplayProperties("Process")
                   .ForField("LevelNumber").DisplayProperties("Level", true)
                   .ForField("DaysBetweenNotifications").DisplayProperties("Days between notifications")
                   .ForField("MaximumNotifications").DisplayProperties("Max notifications to next approver")
                   .ForField("MinimumApprovers").DisplayProperties("Minimum Approvers", true)
                   .ForField("CanSkip").DisplayProperties("Allow Skip Level")
                   .ForField("ApprovalType").DisplayProperties("Approval Type", true)
                   .ForField("DepartmentId").DisplayProperties("Department");
        }
    }
}
