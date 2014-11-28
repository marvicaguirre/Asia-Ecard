using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Wizardsgroup.Core.Web.Infrastructure.Storage
{
    public class StatefulStoragePerSession : DictionaryStatefulStorage
    {        
        public StatefulStoragePerSession()
            : base((key) => HttpContext.Current.Session[key],
                (key, value) => HttpContext.Current.Session[key] = value,
                (key) => RemoveSession()(key, HttpContext.Current.Session.Keys, HttpContext.Current.Session.Remove)) { }

        public StatefulStoragePerSession(HttpSessionStateBase session)
            : base((key) => session[key],
                (key, value) => session[key] = value,
                (key) => RemoveSession()(key, session.Keys,session.Remove)) { }

        private static Action<string, NameObjectCollectionBase.KeysCollection,Action<string>> RemoveSession()
        {
            return (key,sessionKey,action) =>
            {
                var keyName = string.Empty;
                foreach (var keyObject in sessionKey)
                {
                    var hasMatchedKey = keyObject.ToString().Split(':').Any(key.Contains);
                    if (!hasMatchedKey) continue;
                    keyName = keyObject.ToString();
                    break;
                }
                action(keyName);
            };
        }
    }
}