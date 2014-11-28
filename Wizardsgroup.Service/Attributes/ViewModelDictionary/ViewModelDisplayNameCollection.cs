using System;
using System.Collections.Generic;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Service.Attributes.ViewModelDictionary
{
    internal class ViewModelDisplayNameCollection : IViewModelDisplayNameCollection
    {
        #region Members
        private readonly Dictionary<string, Dictionary<string, string>> _dictionaryOfDisplayNames; 
        #endregion

        #region Constructor
        public ViewModelDisplayNameCollection()
        {
            if (_dictionaryOfDisplayNames == null)
                _dictionaryOfDisplayNames = new Dictionary<string, Dictionary<string, string>>();
        } 
        #endregion

        #region Get Key
        bool IViewModelDisplayNameCollection.TryGetDisplayName(Type containerType, string propertyName, out string displayName)
        {                        
            Dictionary<string, string> keyDictionary;
            displayName = null;
            var tryGetValueResult = _dictionaryOfDisplayNames.TryGetValue(ContainerKeyName(containerType), out keyDictionary);
            return tryGetValueResult && TryGetDisplayNameInInnderDictionary(propertyName, keyDictionary, out displayName);
        }

        bool IViewModelDisplayNameCollection.CollectionHasEntries(Type containerType)
        {            
            Dictionary<string, string> keyDictionary;
            var tryGetValueResult = _dictionaryOfDisplayNames.TryGetValue(ContainerKeyName(containerType), out keyDictionary);
            return tryGetValueResult && keyDictionary.Count > 0;
        }

        void IViewModelDisplayNameCollection.ResetEntries()
        {
            _dictionaryOfDisplayNames.Clear();
        }

        #endregion

        #region Add DisplayName
        void IViewModelDisplayNameCollection.TryAddDisplayName(Type containerType, string propertyName, string displayName)
        {
            var innerDictionary = CreateOrUpdateInnerDictionary(containerType, propertyName, displayName);
            CreateOrUpdateKeyValuePair(propertyName, displayName, innerDictionary);            
        }       
        #endregion

        #region Private Functions
        private string ContainerKeyName(Type containerType)
        {
            //hack fix for Validators
            var containerName = containerType.IsInterface ? containerType.Name.Remove(0, 1) : containerType.Name;
            if (!containerName.Contains("ViewModel"))
                containerName = string.Format("{0}ViewModel", containerName);

            return containerName;
        }

        private static bool TryGetDisplayNameInInnderDictionary(string propertyName, IReadOnlyDictionary<string, string> keyDictionary, out string displayName)
        {
            string keyValue;
            var result = keyDictionary.TryGetValue(propertyName, out keyValue);
            displayName = keyValue;
            return result;
        }

        private Dictionary<string, string> CreateOrUpdateInnerDictionary(Type containerType, string propertyName, string displayName)
        {
            Dictionary<string, string> existingInnerDictionary;
            _dictionaryOfDisplayNames.TryGetValue(ContainerKeyName(containerType), out existingInnerDictionary);
            return existingInnerDictionary ?? InitializeAndAddEntryToInnerDictionary(containerType, propertyName, displayName);
        }

        private Dictionary<string, string> InitializeAndAddEntryToInnerDictionary(Type containerType, string propertyName, string displayName)
        {
            var keyDictionary = new Dictionary<string, string> {{propertyName, displayName}};
            _dictionaryOfDisplayNames.Add(ContainerKeyName(containerType), keyDictionary);
            return keyDictionary;
        }

        private void CreateOrUpdateKeyValuePair(string propertyName, string displayName, IDictionary<string, string> keyDictionary)
        {            
            string keyValue;
            var hasKeyInDictionary = keyDictionary.TryGetValue(propertyName, out keyValue);
            if (hasKeyInDictionary)
            {
                keyDictionary[propertyName] = displayName;
            }
            else
            {
                keyDictionary.Add(propertyName, displayName);
            }
        }
        #endregion
    }
}