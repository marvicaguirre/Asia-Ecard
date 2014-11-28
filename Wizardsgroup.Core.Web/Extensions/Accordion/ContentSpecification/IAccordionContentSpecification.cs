namespace Wizardsgroup.Core.Web.Extensions
{
    internal interface IAccordionContentSpecification
    {
        bool IsSatisifiedBy(AccordionItemContainer container);
        string GetContent(AccordionItemContainer container);
    }
}