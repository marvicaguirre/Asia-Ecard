using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Service.Attributes.ViewModelDictionary
{
    public sealed class ViewModelMetadataAttributeOverrider : IViewModelMetadataAttributeOverrider
    {
        #region Members
        private readonly Expression<Func<IEntityService<DataDictionary>>> _lazyLoadedService;
        private Expression<Func<Type>> _containerTypeExpression;
        private Expression<Func<string>> _propertyName;
        private Expression<Func<ICollection<Attribute>>> _attributeCollectionExpression;
        private static IViewModelDisplayNameCollection DisplayNameCollection { get; set; }
        #endregion

        #region Static Func for Validator
        public static string DisplayNameResolver(Type containerType, MemberInfo memberInfo, LambdaExpression ruleForExpression)
        {
            string displayName;
            var result = DisplayNameCollection.TryGetDisplayName(containerType, memberInfo.Name, out displayName);
            return result ? displayName : memberInfo.Name;
        }
        #endregion

        #region Constructor
        public ViewModelMetadataAttributeOverrider(Expression<Func<IEntityService<DataDictionary>>> lazyLoadedService)
        {
            lazyLoadedService.Guard(string.Format("{0}", typeof(Expression<Func<IEntityService<DataDictionary>>>)));
            _lazyLoadedService = lazyLoadedService;
            DisplayNameCollection = new ViewModelDisplayNameCollection();
        }
        #endregion

        #region Public Functions/Methods
        public IViewModelMetadataAttributeOverrider RegisterAttributes(Expression<Func<ICollection<Attribute>>> attributeCollectionExpression)
        {
            _attributeCollectionExpression = attributeCollectionExpression;
            return this;
        }

        public IViewModelMetadataAttributeOverrider RegisterContainerType(Expression<Func<Type>> containerTypeExpression)
        {
            _containerTypeExpression = containerTypeExpression;
            return this;
        }

        public IViewModelMetadataAttributeOverrider RegisterPropertyName(Expression<Func<string>> propertyName)
        {
            _propertyName = propertyName;
            return this;
        }

        public IEnumerable<Attribute> OverridePropertyAttributes()
        {
            var wrapper = CompileAndWrap();
            var validViewModel = IsValidViewModel(wrapper);
            var addAttribute = IsAttributeClearForAdd(wrapper);
            if (validViewModel && addAttribute)
            {
                var overriders = CreateMetaDataAttributeOverrider(wrapper);
                return overriders.Find(o => o.IsMatched(wrapper.Container)).OverrideMetaDataAttribute();
            }
            return wrapper.Attributes;
        }
        #endregion

        #region Private Function/Methods

        private bool IsValidViewModel(ViewModelMetadataWrapper wrapper)
        {
            return wrapper.Container != null 
                //&& wrapper.Container.Name.Contains("ViewModel") 
                && !string.IsNullOrEmpty(wrapper.PropertyName);
        }

        private bool IsAttributeClearForAdd(ViewModelMetadataWrapper wrapper)
        {
            return !wrapper.Attributes.Any(attribute => attribute is DisplayAttribute);
        }

        private ViewModelMetadataWrapper CompileAndWrap()
        {
            var attr = _attributeCollectionExpression.Compile().Invoke();
            var containerType = _containerTypeExpression.Compile().Invoke();
            var propertyName = _propertyName.Compile().Invoke();
            return new ViewModelMetadataWrapper(containerType, propertyName, attr);
        }

        private List<AbstractViewModelMetadataDictionary> CreateMetaDataAttributeOverrider(ViewModelMetadataWrapper wrapper)
        {
            var viewModelDictionary = new List<AbstractViewModelMetadataDictionary>
                {
                    new ViewModelMetadataDictionaryLoader(_lazyLoadedService,DisplayNameCollection, wrapper),
                    new ViewModelMetadataDictionaryCached(_lazyLoadedService,DisplayNameCollection, wrapper)
                };
            return viewModelDictionary;
        }
        #endregion
    }
}