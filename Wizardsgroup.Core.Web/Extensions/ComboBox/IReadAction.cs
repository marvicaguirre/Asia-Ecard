namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IReadAction
    {
        IReadAction Action(string action, string controller);
        IReadAction Parameter(params string[] paramControls);
    }
}