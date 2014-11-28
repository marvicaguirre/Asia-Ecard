using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Wizardsgroup.Domain.Interfaces
{
    public interface IViewModelMetadataAttributeOverrider
    {
        IViewModelMetadataAttributeOverrider RegisterAttributes(Expression<Func<ICollection<Attribute>>> attributeCollectionExpression);
        IViewModelMetadataAttributeOverrider RegisterContainerType(Expression<Func<Type>> containerTypeExpression);
        IViewModelMetadataAttributeOverrider RegisterPropertyName(Expression<Func<string>> propertyName);        
        IEnumerable<Attribute> OverridePropertyAttributes();
    }
}