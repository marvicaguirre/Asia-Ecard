using System;

namespace Wizardsgroup.Core.Web.Infrastructure.Storage
{
    public interface IStatefulStorage
    {
        TValue Get<TValue>(string name);
        TValue GetOrAdd<TValue>(string name, Func<TValue> valueFactory);
        void Remove(string name);
    }
}