using System;
using System.Collections;

namespace Wizardsgroup.Core.Web.Infrastructure.Storage
{
    public abstract class DictionaryStatefulStorage : IStatefulStorage
    {
        readonly Func<string, object> _getter;
        readonly Action<string, object> _setter;
        readonly Action<string> _remove;

        protected DictionaryStatefulStorage(Func<IDictionary> dictionaryAccessor)
        {
            _getter = key => dictionaryAccessor()[key];
            _setter = (key, value) => dictionaryAccessor()[key] = value;
        }

        protected DictionaryStatefulStorage(Func<string, object> getter, Action<string, object> setter, Action<string> remove = null)
        {
            _getter = getter;
            _setter = setter;
            _remove = remove;
        }

        protected static string FullNameOf(Type type, string name)
        {
            string fullName = type.FullName;
            if (!String.IsNullOrWhiteSpace(name))
                fullName += "::" + name;

            return fullName;
        }

        public TValue Get<TValue>(string name)
        {
            return (TValue)_getter(FullNameOf(typeof(TValue), name));
        }

        public TValue GetOrAdd<TValue>(string name, Func<TValue> valueFactory)
        {
            string fullName = FullNameOf(typeof(TValue), name);
            TValue result = (TValue)_getter(fullName);

            if (Equals(result, default(TValue)))
            {
                result = valueFactory();
                _setter(fullName, result);
            }

            return result;
        }

        public void Remove(string name)
        {
            if (_remove != null)
                _remove(name);
        }
    }
}