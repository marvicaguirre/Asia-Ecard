using System.Web;

namespace Wizardsgroup.Core.Web.Infrastructure.Storage
{
    public class StatefulStoragePerRequest : DictionaryStatefulStorage
    {
        public StatefulStoragePerRequest()
            : base(() => HttpContext.Current.Items) { }
        
        public StatefulStoragePerRequest(HttpContextBase context)
            : base(() => context.Items) { }
    }
}