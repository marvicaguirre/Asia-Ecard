using System;
using System.Collections.Generic;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Domain.Lookup;

namespace Wizardsgroup.Service.Lookup
{
    internal class NullCustomLookup : ICustomLookup
    {
        public NullCustomLookup()
        {
            Value = "Value";
            Text = "Text";
        }
        public string Value { get; private set; }
        public string Text { get; private set; }

        public IEnumerable<ILookupValueField> GetRecordsForLookup()
        {
            return GetNullLookupData();
        }

        public IEnumerable<ILookupValueField> GetRecordsForCascade(int id)
        {
            return GetRecordsForLookup();
        }

        public object Specification { get; set; }
        public string TextFilter { get; set; }

        private IEnumerable<ILookupValueField> GetNullLookupData()
        {
            return new List<LookupData>
            {
                new LookupData("No lookup found for setting provided.", Guid.Empty.ToString())
            };
        }
    }
}
