namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ITargetFieldMode
    {
        ITargetFieldSingleSetting Single();
        bool List();
    }
}