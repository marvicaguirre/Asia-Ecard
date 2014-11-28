using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public interface ISessionKeyCollection
    {
        void TryGetKey<T>(T controller,string key, out object value) where T : Controller;
        void TryAddKey<T>(T controller, string key, object value) where T : Controller;
        void TryRemoveKey<T>(T controller, string key) where T : Controller;
    }
}