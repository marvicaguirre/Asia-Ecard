namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IClientEvent
    {
        IClientEvent Change(string change);
        IClientEvent Blur(string blur);
    }
}