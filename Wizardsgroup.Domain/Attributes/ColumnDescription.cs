using System;

namespace Wizardsgroup.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnDescription : System.Attribute
    {
        public string Description { get; private set; }
        public string SampleData { get; set; }

        public ColumnDescription(string description)
        {
            Description = description;
        }
    }
}
