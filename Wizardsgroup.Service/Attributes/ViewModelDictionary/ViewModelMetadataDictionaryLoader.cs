using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Service.Attributes.ViewModelDictionary
{
    internal class ViewModelMetadataDictionaryLoader : AbstractViewModelMetadataDictionary
    {
        public ViewModelMetadataDictionaryLoader(Expression<Func<IEntityService<DataDictionary>>> lazyLoadedService, IViewModelDisplayNameCollection displayNameCollection, ViewModelMetadataWrapper viewModelMetaDataWrapper) : base(lazyLoadedService, displayNameCollection, viewModelMetaDataWrapper)
        {
        }

        public override bool IsMatched(Type containerType)
        {
            return !DisplayNameCollection.CollectionHasEntries(containerType);
        }

        public override IEnumerable<Attribute> OverrideMetaDataAttribute()
        {
            LoadDictionary(ViewModelMetaDataWrapper.Container);
            return AddAndReturnAttributes();
        }

        #region Private Functions/Methods
        private void LoadDictionary(Type containerType)
        {
            Service.Filter(DictionaryFilter(containerType)).ToList().ForEach(dataDictionary => LoadToDictionaryTheResult(dataDictionary, containerType));
        }

        private IEnumerable<Attribute> AddAndReturnAttributes()
        {
            string displayName;
            if (DisplayNameCollection.TryGetDisplayName(ViewModelMetaDataWrapper.Container, ViewModelMetaDataWrapper.PropertyName, out displayName))
                ViewModelMetaDataWrapper.Attributes.Add(new DisplayAttribute { Name = displayName });

            return ViewModelMetaDataWrapper.Attributes;
        }

        private Expression<Func<DataDictionary, bool>> DictionaryFilter(Type containerType)
        {
            return o => o.Model.ToLower() == containerType.Name.ToLower();
        }

        private void LoadToDictionaryTheResult(DataDictionary dataDictionary, Type containerType)
        {
            DisplayNameCollection.TryAddDisplayName(containerType, dataDictionary.FieldName, dataDictionary.FieldDisplayText);
        } 
        #endregion
    }
}
