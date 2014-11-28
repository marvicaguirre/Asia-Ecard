using System;
using System.Web;
using System.Web.SessionState;
using Wizardsgroup.Core.Web.Constants;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public static class SessionManager
    {
        public static void ClearSession()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            }

            IHttpSessionState sessionState = GetSessionStateObject();
            sessionState.Clear();
        }

        public static string GetUserName()
        {
            var container = GetUserSessionContainer();
            return container != null ? container.UserName : string.Empty;
        }

        public static int GetUserId()
        {
            var container = GetUserSessionContainer();
            return container != null ? container.UserId : 0;
        }

        public static ISessionContainer GetUserSessionContainer()
        {
            IHttpSessionState sessionState = GetSessionStateObject();
            var userSessionContainer = sessionState[GetKey()] as ISessionContainer;
            return userSessionContainer;
        }

        public static void CheckSession()
        {
            if (!IsSessionEstablished())
            {
                throw new Exception(ErrorMessage.UnauthorizedAccess);
            }
        }

        /// <summary>
        /// Check if the user has an existing session.
        /// </summary>
        /// <returns></returns>
        public static bool IsSessionEstablished()
        {
            return GetUserSessionContainer() != null;
        }

        public static void StoreUserSessionContainer(ISessionContainer userSessionContainer)
        {
            IHttpSessionState sessionState = GetSessionStateObject();
            sessionState[GetKey()] = userSessionContainer;
        }

        private static string GetKey()
        {
            string sessionId = GetSessionStateObject().SessionID;
            //string key = SessionVariableContainerType.UserSessionContainer.ToString() + sessionId;
            //return key;
            return sessionId;

        }

        private static IHttpSessionState GetSessionStateObject()
        {
            IHttpSessionState sessionState = new SessionWrapper();
            return sessionState;
        }
    }
}