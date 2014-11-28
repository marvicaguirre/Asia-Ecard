using System;
using System.Collections.Concurrent;

namespace Wizardsgroup.Utilities.Helpers
{
    public static class Singleton<T> where T : class
    {        
        static readonly ConcurrentDictionary<Type, T> _allSingletons;
        static readonly object SyncLock = new object();
        static volatile T _instance = ReflectionHelper.Instance.CreateInstanceOfType<T>(typeof (T));

        static Singleton()
        {
            _allSingletons = new ConcurrentDictionary<Type, T>();
        }

        public static T Instance
        {
            get
            {
                var hasKey = _allSingletons.ContainsKey(typeof (T));
                if (hasKey) return _allSingletons[typeof (T)];

                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _allSingletons[typeof(T)] = ReflectionHelper.Instance.CreateInstanceOfType<T>(typeof(T));          
                        }
                    }
                }
                else
                {
                    _allSingletons[typeof (T)] = _instance;
                }
                return _allSingletons[typeof(T)];
            }
        }
    }
}
