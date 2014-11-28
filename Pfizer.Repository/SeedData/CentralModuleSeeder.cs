using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Security;
using Pfizer.Domain.Infrastructure;

namespace Pfizer.Repository.SeedData
{
    internal class CentralModuleSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var comparer = new ModuleNameComparer();
            var distinctModules = new FunctionHelper(RegisterModuleFunctionContainer.Instance.Container)
                .GetFunctionNames()
                .Distinct(comparer);
            
            foreach (var distinctModule in distinctModules)
            {
                var record = new CentralModule
                    {                        
                        ModuleName = distinctModule.FullModuleName,
                        DisplayName = distinctModule.FullModuleDisplayName,
                        ShortDisplayName = distinctModule.FullModuleName,
                        CreatedBy = "System"                        
                    };
                context.EntitySet<CentralModule>().AddOrUpdate(c => new { c.ModuleName } , record);
            }

            context.SaveChanges();
        }
    }
}
