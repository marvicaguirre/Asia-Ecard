using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITemplateIndexRegistrator
    {
        ITemplateIndexParentRegistrator Collection(string name, Guid indexId);
        ITemplateIndexRegistrator SkipIndexIdentifierInView();
    }
}