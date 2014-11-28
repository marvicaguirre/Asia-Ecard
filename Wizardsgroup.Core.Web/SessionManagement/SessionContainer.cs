using System;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public class SessionContainer : ISessionContainer
    {
        #region Properties
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; private set; }
        //public CompanyTypeEnum Role { get; set; }
        public IUserInfo UserInfo { get; set; }
        public ISessionKeyCollection KeyCollection { get; set; }
        #endregion        

        public SessionContainer(string sessiondId, ISessionKeyCollection keyCollection)
        {
            if (string.IsNullOrEmpty(sessiondId))
                throw new ArgumentNullException("sessiondId");

            if (keyCollection == null)
                throw new ArgumentNullException("keyCollection");

            KeyCollection = keyCollection;
            SessionId = sessiondId;
        }
    }
}