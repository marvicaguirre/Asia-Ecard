using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class DosageDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("DosageViewModel")
                .ForField("ProductName").DisplayProperties("Product")
                .ForField("UniqueId").DisplayProperties("Dosage ID", true)
                .ForField("Name").DisplayProperties("Dosage Form", true);
        }
    }
}
