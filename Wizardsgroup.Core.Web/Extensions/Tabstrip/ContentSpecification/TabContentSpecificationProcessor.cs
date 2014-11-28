using System.Collections.Generic;
using System.Linq;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TabContentSpecificationProcessor : ITabContentSpecificationProcessor
    {
        public string GetContent(TabstripItemContainer container)
        {
            var contentSpecifications = GetContentSpecifications();
            var contentSpecifier = contentSpecifications.First(specification => specification.IsSatisifiedBy(container));
            return contentSpecifier.GetContent(container);
        }

        private IEnumerable<ITabContentSpecification> GetContentSpecifications()
        {
            return new List<ITabContentSpecification>
                {
                    new FuncTabContentSpecification(),
                    new MvcHtmlStringTabContentSpecification(),
                    //this should be always the last entry
                    new NullTabContentSpecification(),
                };
        }
    }
}