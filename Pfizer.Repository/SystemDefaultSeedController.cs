using System.Collections.Generic;
using Pfizer.Repository.SeedData;
using Wizardsgroup.Repository;

namespace Pfizer.Repository
{
    internal class SystemDefaultSeedController
    {
        public List<IDataSeeder> GetSeeders()
        {
            return new List<IDataSeeder>
                {      
                    //=======System Messages========//
                    new SystemSettingSeeder(),
                    new SystemMessageSeeder(),
                    //=======Security========//
                    new CentralModuleSeeder(),
                    new CentralFunctionSeeder(),
                    new UserSeeder(),
                    new UserGroupSeeder(),
                    new UserGroupMapSeeder(),
                    new UserGroupFuntionSeeder(),
                    //======Company Setup========//
                    new CompanyClassificationSeeder(),
                    new CompanySeeder(),
                    new EmployeeTypeSeeder(),
                    new DepartmentSeeder(),
                    new ProcessSeeder(),
                    new EmployeeSeeder(),
                };
        }
    }
}
