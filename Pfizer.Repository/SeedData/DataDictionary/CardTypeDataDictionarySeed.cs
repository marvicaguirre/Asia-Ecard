using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository.DataDictionaryBuilder;

namespace Pfizer.Repository.SeedData.DataDictionary
{
    internal class CardTypeDataDictionarySeeder : AbstractDataDictionarySeeder
    {
        protected override void SetupDictionary(IDataDictionaryBuilder builder)
        {
            builder.ForViewModel("CardTypeViewModel")
                .ForField("ClassName").DisplayProperties("Class")
                .ForField("Name").DisplayProperties("Card Type", true)
                .ForField("Description").DisplayProperties("Description", true);
        }
    }
}
