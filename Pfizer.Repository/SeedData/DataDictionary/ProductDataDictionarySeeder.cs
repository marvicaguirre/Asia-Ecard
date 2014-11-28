using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class ProductDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("ProductViewModel")
                .ForField("Code").DisplayProperties("Product Code", true)
                .ForField("Name").DisplayProperties("Product", true)
                .ForField("Description").DisplayProperties("Description", true);
        }
    }
}
