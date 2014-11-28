namespace Wizardsgroup.Core.Web.Extensions
{
    internal class NullContentSpecification : IAccordionContentSpecification
    {
        public bool IsSatisifiedBy(AccordionItemContainer container)
        {
            return true;
        }

        public string GetContent(AccordionItemContainer container)
        {
            return "<br/><p>No content configuration found. Please set a content configuration.</p><br/>";
        }
    }
}