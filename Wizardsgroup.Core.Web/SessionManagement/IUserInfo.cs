using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public interface IUserInfo
    {
        AbstractEmployee Employee { get; }
        //Company Company { get; }
        //List<Guid> Roles { get; }
    }
}