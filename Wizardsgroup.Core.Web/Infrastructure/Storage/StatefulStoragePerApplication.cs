using System.Web;

namespace Wizardsgroup.Core.Web.Infrastructure.Storage
{
    public class StatefulStoragePerApplication : DictionaryStatefulStorage
    {        
        public StatefulStoragePerApplication()
            : base((key) => HttpContext.Current.Application[key],
                (key, value) => HttpContext.Current.Application[key] = value,
                (key) => HttpContext.Current.Application.Remove(key)) { }
        
        public StatefulStoragePerApplication(HttpApplicationStateBase app)
            : base((key) => app[key],
                (key, value) => app[key] = value,
                app.Remove) { }
    }
}