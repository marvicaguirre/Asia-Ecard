using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TemplateIndexRegistrator : ITemplateIndexRegistrator
    {
        internal string Name { get; set; }
        internal Guid IndexId { get; set; }
        internal bool SkipIndexIdentifierId { get; set; }
        internal TemplateIndexParentRegistrator RegistratorContainer { get; set; }

        public ITemplateIndexParentRegistrator Collection(string name, Guid indexId)
        {
            Name = name;
            IndexId = indexId;
            RegistratorContainer = new TemplateIndexParentRegistrator(this);
            return RegistratorContainer;
        }

        public ITemplateIndexRegistrator SkipIndexIdentifierInView()
        {
            SkipIndexIdentifierId = true;
            return this;
        }
    }
}