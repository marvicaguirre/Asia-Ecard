using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Service.Attributes.ViewModelDictionary
{
    internal class ViewModelMetadataDictionaryCached : AbstractViewModelMetadataDictionary
    {
        public ViewModelMetadataDictionaryCached(Expression<Func<IEntityService<DataDictionary>>> lazyLoadedService, IViewModelDisplayNameCollection displayNameCollection, ViewModelMetadataWrapper viewModelMetaDataWrapper) : base(lazyLoadedService, displayNameCollection, viewModelMetaDataWrapper)
        {
        }

        public override bool IsMatched(Type containerType)
        {
            return DisplayNameCollection.CollectionHasEntries(containerType);
        }

        public override IEnumerable<Attribute> OverrideMetaDataAttribute()
        {
            string displayName;
            if (DisplayNameCollection.TryGetDisplayName(ViewModelMetaDataWrapper.Container, ViewModelMetaDataWrapper.PropertyName, out displayName))
                ViewModelMetaDataWrapper.Attributes.Add(new DisplayAttribute { Name = displayName });

            return ViewModelMetaDataWrapper.Attributes;
        }
    }
}
