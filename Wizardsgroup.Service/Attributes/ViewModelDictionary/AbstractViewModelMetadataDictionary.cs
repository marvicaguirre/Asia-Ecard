using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Service.Attributes.ViewModelDictionary
{
    internal abstract class AbstractViewModelMetadataDictionary
    {
        #region Members
        private readonly Expression<Func<IEntityService<DataDictionary>>> _lazyLoadedService;
        private WeakReference<IEntityService<DataDictionary>> _serviceBackingField;        
        #endregion

        #region Properties
        internal IEntityService<DataDictionary> Service { get { return WeakReferencedDataDictionaryService();} }
        internal IViewModelDisplayNameCollection DisplayNameCollection { get; private set; }
        internal ViewModelMetadataWrapper ViewModelMetaDataWrapper { get; private set; }
        #endregion

        #region Constructor
        protected AbstractViewModelMetadataDictionary(Expression<Func<IEntityService<DataDictionary>>> lazyLoadedService, IViewModelDisplayNameCollection displayNameCollection, ViewModelMetadataWrapper viewModelMetaDataWrapper)
        {
            displayNameCollection.Guard(string.Format("{0}.", typeof(IViewModelDisplayNameCollection).Name));
            viewModelMetaDataWrapper.Guard(string.Format("{0}.", typeof(ViewModelMetadataWrapper).Name));

            _lazyLoadedService = lazyLoadedService;
            ViewModelMetaDataWrapper = viewModelMetaDataWrapper;
            DisplayNameCollection = displayNameCollection;
        }
        #endregion

        #region Overrides
        public abstract bool IsMatched(Type containerType);
        public abstract IEnumerable<Attribute> OverrideMetaDataAttribute();
        #endregion

        #region Private Functions/Methods
        private IEntityService<DataDictionary> WeakReferencedDataDictionaryService()
        {
            IEntityService<DataDictionary> service;
            if (_serviceBackingField == null)
            {
                SetEntityService(out service);
                return service;
            }

            _serviceBackingField.TryGetTarget(out service);
            if (service == null)
            {
                SetEntityService(out service);
                return service;
            }
            return service;
        }

        private void SetEntityService(out IEntityService<DataDictionary> service)
        {
            service = _lazyLoadedService.Compile().Invoke();
            _serviceBackingField = new WeakReference<IEntityService<DataDictionary>>(service);
        } 
        #endregion
    }
}
