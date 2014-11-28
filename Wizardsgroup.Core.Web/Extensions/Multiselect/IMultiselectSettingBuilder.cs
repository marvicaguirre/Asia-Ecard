namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IMultiselectSettingBuilder
    {
        IMultiselectSettingBuilder Delimeter(string delimeter);
        IMultiselectSettingBuilder Filterable(bool isFilterable);
        IMultiselectSettingBuilder PlaceHolder(string placeHolder);
        IMultiselectSettingBuilder Width(int width);
        IMultiselectSettingBuilder SelectAll(bool hasSelectAll);
    }
}