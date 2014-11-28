using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class UnitOfMeasureDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("UnitOfMeasureViewModel")
                .ForField("Name").DisplayProperties("Unit of Measure", true)
                .ForField("Description").DisplayProperties("Description", true);
        }
    }
}
