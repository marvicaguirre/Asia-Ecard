using System;
using System.Collections.Generic;
using System.Linq;

namespace Wizardsgroup.Utilities.Helpers
{
    public sealed class UserTracker
    {
        private readonly Dictionary<string,string> _userNames;
        private Func<string> _getSessionId;
        private UserTracker()
        {
            _userNames = new Dictionary<string, string> {{"System", "System"}};
        }

        public static UserTracker Instance
        {
            get { return Singleton<UserTracker>.Instance; }   
        }

        public void SetSessionDelegateGetter(Func<string> getSessionId)
        {
            _getSessionId = getSessionId;
        }

        public void SetUserName(string sessionId ,string userName)
        {
            var newPair = _userNames.Any(o => o.Key == sessionId && o.Value != userName);
            if (newPair) _userNames.Remove(sessionId);
            var hasExistingPair = _userNames.Any(o => o.Key == sessionId && o.Value == userName);
            if (hasExistingPair) return;
            _userNames.Add(sessionId,userName);
        }

        public string GetUserName()
        {
            Func<string> defaultSessionGetter = () => "System";
            Func<string> getSessionId = _getSessionId ?? defaultSessionGetter;
            var key = getSessionId();
            string userName = _userNames.FirstOrDefault(o => o.Key == key).Value;
            return userName;
        }

        public void ClearUserName()
        {
            try
            {
                _userNames.Remove(_getSessionId());
            }
            catch
            {
                                
            }
            
        }
    }
}
