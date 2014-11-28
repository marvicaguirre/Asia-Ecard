using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITemplateIndexParentRegistrator
    {
        ITemplateIndexParentRegistrator ParentCollection(string name, Guid indexId, int order);
    }
}