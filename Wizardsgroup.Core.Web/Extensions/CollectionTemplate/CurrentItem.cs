using System;
using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CurrentItem
    {
        public CurrentItem()
        {
            ContainerHolder = new List<ContainerHolder>();
        }
        public string Name { get; set; }
        public Guid IndexId { get; set; }
        public bool SkipIndexIdentifier { get; set; }
        public List<ContainerHolder> ContainerHolder;
    }
}