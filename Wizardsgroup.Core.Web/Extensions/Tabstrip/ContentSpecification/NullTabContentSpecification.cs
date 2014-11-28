namespace Wizardsgroup.Core.Web.Extensions
{
    internal class NullTabContentSpecification : ITabContentSpecification
    {
        public bool IsSatisifiedBy(TabstripItemContainer container)
        {
            return true;
        }

        public string GetContent(TabstripItemContainer container)
        {
            return "<br/><p>No content configuration found. Please set a content configuration.</p><br/>";
        }
    }
}