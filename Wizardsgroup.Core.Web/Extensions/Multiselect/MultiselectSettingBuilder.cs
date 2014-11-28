namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MultiselectSettingBuilder : IMultiselectSettingBuilder
    {
        internal MultiselectSettingContainer Container = new MultiselectSettingContainer();

        public IMultiselectSettingBuilder Delimeter(string delimeter)
        {
            Container.Delimeter = delimeter;
            return this;
        }

        public IMultiselectSettingBuilder Filterable(bool isFilterable)
        {
            Container.IsFilterable = isFilterable;
            return this;
        }

        public IMultiselectSettingBuilder PlaceHolder(string placeHolder)
        {
            Container.PlaceHolder = placeHolder;
            return this;
        }

        public IMultiselectSettingBuilder Width(int width)
        {
            Container.Width = width;
            return this;
        }

        public IMultiselectSettingBuilder SelectAll(bool hasSelectAll)
        {
            Container.SelectAll = hasSelectAll;
            return this;
        }
    }
}