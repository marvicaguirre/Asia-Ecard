using System;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public interface ISessionContainer
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string SessionId { get; }
        IUserInfo UserInfo { get; set; }
        ISessionKeyCollection KeyCollection { get; }
    }
}
