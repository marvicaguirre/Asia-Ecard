namespace Wizardsgroup.Domain.Interfaces
{
    public interface IMultiSelectLookupValueField : ILookupValueField
    {        
        bool IsSelected { get; set; }
    }
}