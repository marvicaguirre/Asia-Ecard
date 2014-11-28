using System.Collections.Generic;

namespace Wizardsgroup.Service.Lookup
{
    public interface IEnumLookupFluentBuilder<TEnum>
    {
        IEnumLookupFluentBuilder<TEnum> SetFlagValueAsValueField();
        IEnumLookupFluentBuilder<TEnum> SetFlagNameAsValueField();
        IEnumLookupFluentBuilder<TEnum> SplitCamelCaseInTextField();
        IEnumLookupFluentBuilder<TEnum> SplitCamelCaseInValueField();
        IEnumLookupFluentBuilder<TEnum> RemoveFlag(TEnum tEnum);
        IEnumLookupFluentBuilder<TEnum> ContainOnly(List<TEnum> tEnums);
        IEnumLookup GenerateLookupData();
    }
}
