namespace Wizardsgroup.Core.Web.Extensions
{
    internal interface ITabContentSpecification
    {
        bool IsSatisifiedBy(TabstripItemContainer container);
        string GetContent(TabstripItemContainer container); 
    }
}