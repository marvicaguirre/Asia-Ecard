using System.Collections.Generic;
using System.Linq;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class AccordionContentSpecificationProcessor : IAccordionContentSpecificationProcessor
    {
        public string GetContent(AccordionItemContainer container)
        {
            var contentSpecifications = GetContentSpecifications();
            var contentSpecifier = contentSpecifications.First(specification => specification.IsSatisifiedBy(container));
            return contentSpecifier.GetContent(container);            
        }

        private IEnumerable<IAccordionContentSpecification> GetContentSpecifications()
        {
            return new List<IAccordionContentSpecification>
                {
                    new FuncContentSpecification(),
                    new MvcHtmlStringContentSpecification(),
                    //this should be always the last entry
                    new NullContentSpecification(),
                };
        }
    }
}