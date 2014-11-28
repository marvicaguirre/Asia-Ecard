using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.ModelMetadataProvider
{
    public sealed class ViewModelCachedDataAnnotationsModelMetadataProvider : CachedDataAnnotationsModelMetadataProvider
    {
        #region Members
        private readonly IViewModelMetadataAttributeOverrider _attributeOverrider;
        #endregion

        #region Constructor
        public ViewModelCachedDataAnnotationsModelMetadataProvider(IViewModelMetadataAttributeOverrider attributeOverrider)
        {
            _attributeOverrider = attributeOverrider;
        }

        #endregion

        #region Overrides
        protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
        {
            attributes = _attributeOverrider
                .RegisterPropertyName(() => propertyName)
                .RegisterContainerType(() => containerType)
                .RegisterAttributes(() => attributes.ToList())
                .OverridePropertyAttributes();

            return base.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);
        }
        #endregion

    }
}