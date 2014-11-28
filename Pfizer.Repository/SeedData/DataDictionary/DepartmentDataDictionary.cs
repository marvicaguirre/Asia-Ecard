using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
   internal class DepartmentDataDictionary : AbstractDataDictionarySeeder
    {
       protected override void SetupDictionary(IDataDictionaryBuilder builder)
       {
           builder.ForViewModel("DepartmentViewModel")
                  .ForField("Name").DisplayProperties("Department", true)
                  .ForField("Description").DisplayProperties("Description", true)
                  .ForField("IsSBD").DisplayProperties("SBD");
       }
    }
}
