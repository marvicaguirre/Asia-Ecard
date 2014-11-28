using System.Collections.Generic;

namespace Wizardsgroup.Utilities.Extensions
{
    public class PropertySetting
    {
        public PropertySetting()
        {
            Properties = new List<string>();
            AdditionPropertyDictionary = new Dictionary<string, object>();
        }
        public List<string> Properties { get; set; }
        public Dictionary<string, object> AdditionPropertyDictionary { get; set; }
    }
}