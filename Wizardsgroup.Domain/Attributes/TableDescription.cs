using System;

namespace Wizardsgroup.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableDescription : System.Attribute
    {
        public string Description { get; private set; }
        
        public TableDescription(string description)
        {
            Description = description;
        }
    }
}
