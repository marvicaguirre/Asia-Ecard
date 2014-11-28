using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TemplateIndexParentRegistrator : ITemplateIndexParentRegistrator
    {        
        internal readonly CurrentItem CurrentItem = new CurrentItem();

        public TemplateIndexParentRegistrator(TemplateIndexRegistrator templateIndexRegistrator)
        {            
            CurrentItem.Name = templateIndexRegistrator.Name;
            CurrentItem.IndexId = templateIndexRegistrator.IndexId;
            CurrentItem.SkipIndexIdentifier = templateIndexRegistrator.SkipIndexIdentifierId;
        }

        public ITemplateIndexParentRegistrator ParentCollection(string name, Guid indexId, int order)
        {
            CurrentItem.ContainerHolder.Add(new ContainerHolder
                {
                    Name = name,
                    IndexId = indexId,
                    Order = order
                });
            return this;
        }
    }
}