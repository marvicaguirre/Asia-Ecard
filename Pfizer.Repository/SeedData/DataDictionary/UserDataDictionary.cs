using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class UserDataDictionary : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("UserViewModel")
                .ForField("UserId").DisplayProperties("User")
                .ForField("EmployeeId").DisplayProperties("Employee", true)
                .ForField("UserName").DisplayProperties("User Name", true)
                .ForField("UserPassword").DisplayProperties("Password", true)
                .ForField("FullName").DisplayProperties("Full Name");
        }
    }
}