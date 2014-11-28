using System;
using System.Collections.Generic;

namespace Wizardsgroup.Service.Attributes.ViewModelDictionary
{
    internal class ViewModelMetadataWrapper
    {
        public Type Container { get; private set; }
        public string PropertyName { get; private set; }
        public ICollection<Attribute> Attributes { get; private set; }

        public ViewModelMetadataWrapper(Type container, string propertyName, ICollection<Attribute> attributes)
        {
            Container = container;
            PropertyName = propertyName;
            Attributes= attributes;
        }
    }
}
