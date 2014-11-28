using Wizardsgroup.Utilities.Security;

namespace Pfizer.Domain.Infrastructure
{
    public interface IRegistrator
    {
        void Register(ISecurityRegistrator registrator);
    }
}