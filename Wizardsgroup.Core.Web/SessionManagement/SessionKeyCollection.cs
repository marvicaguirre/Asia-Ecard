using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public class SessionKeyCollection : ISessionKeyCollection
    {
        #region Members
        private readonly Dictionary<Type, Dictionary<string, object>> _controllerSessionKeys; 
        #endregion

        #region Constructor
        public SessionKeyCollection()
        {
            if (_controllerSessionKeys == null)
                _controllerSessionKeys = new Dictionary<Type, Dictionary<string, object>>();
        } 
        #endregion

        #region Get Key
        public void TryGetKey<T>(T controller,string key, out object value) where T : Controller
        {
            value = null;
            Dictionary<string, object> controllerKeyDictionary;
            var hasControllerDictionary = _controllerSessionKeys.TryGetValue(controller.GetType(), out controllerKeyDictionary);
            if (hasControllerDictionary)
            {
                object keyValue;
                controllerKeyDictionary.TryGetValue(key, out keyValue);
                value = keyValue;
            }
        } 
        #endregion

        #region Add Key
        public void TryAddKey<T>(T controller, string key, object value) where T : Controller
        {
            var controllerKeyDictionary = _CreateOrUpdateControllerKeyDictionary(controller.GetType(), key, value);
            _CreateOrUpdateKey(key, value, controllerKeyDictionary);
        }
        public void TryRemoveKey<T>(T controller, string key) where T : Controller
        {
            _DeleteControllerKeyDictionary(controller.GetType(), key);
        }
        #endregion

        #region Private Functions
        private Dictionary<string, object> _DeleteControllerKeyDictionary(Type controller, string key)
        {
            Dictionary<string, object> controllerKeyDictionary;
            var hasTypeInDictionaryController = _controllerSessionKeys.TryGetValue(controller, out controllerKeyDictionary);
            if (hasTypeInDictionaryController)
            {
                controllerKeyDictionary.Remove(key);
            }
            return controllerKeyDictionary;
        } 
        private Dictionary<string, object> _CreateOrUpdateControllerKeyDictionary(Type controller, string key, object value)
        {
            Dictionary<string, object> controllerKeyDictionary;
            var hasTypeInDictionaryController = _controllerSessionKeys.TryGetValue(controller, out controllerKeyDictionary);
            if (!hasTypeInDictionaryController)
            {
                controllerKeyDictionary = new Dictionary<string, object> { { key, value } };
                _controllerSessionKeys.Add(controller, controllerKeyDictionary);
            }
            return controllerKeyDictionary;
        } 

        private static void _CreateOrUpdateKey(string key, object value, Dictionary<string, object> controllerKeyDictionary)
        {
            object keyValue;
            var hasKeyInDictionary = controllerKeyDictionary.TryGetValue(key, out keyValue);
            if (!hasKeyInDictionary)
            {
                controllerKeyDictionary.Add(key, value);
            }

            controllerKeyDictionary[key] = value;
        }
        #endregion
    }
}