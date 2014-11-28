using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Security;
using Pfizer.Domain.Infrastructure;

namespace Pfizer.Repository.SeedData
{
    internal class CentralFunctionSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var distinctModules = new FunctionHelper(RegisterModuleFunctionContainer.Instance.Container).GetFunctionNames();

            foreach (var distinctModule in distinctModules)
            {
                var moduleName = distinctModule.FullModuleName;
                var centralModuleId = from cm in context.EntitySet<CentralModule>()
                                      where cm.ModuleName == moduleName
                                      select cm.CentralModuleId;

                var record = new CentralFunction
                {                                        
                    FunctionName = distinctModule.FunctionName,
                    DisplayName = distinctModule.FunctionDisplayName,
                    ShortDisplayName = distinctModule.FunctionShortName,
                    CentralModuleId = centralModuleId.First(),
                    CreatedBy = "System"
                };
                context.EntitySet<CentralFunction>().AddOrUpdate(c => new { c.FunctionName } , record);
            }
            context.SaveChanges();
        }
    }
}
