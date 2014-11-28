using System;

namespace Wizardsgroup.Domain.Interfaces
{
    public interface IViewModelDisplayNameCollection
    {
        bool TryGetDisplayName(Type containerType,string propertyName, out string displayName);
        void TryAddDisplayName(Type containerType, string propertyName, string displayName);
        bool CollectionHasEntries(Type containerType);
        void ResetEntries();        
    }
}