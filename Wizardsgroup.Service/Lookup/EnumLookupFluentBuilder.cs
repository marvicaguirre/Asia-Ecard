using System;
using System.Collections.Generic;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Service.Lookup
{
    public class EnumLookupFluentBuilder<TEnum> : IEnumLookupFluentBuilder<TEnum>
    {
        private readonly EnumLookup _lookup;
        internal List<string> FlagsToRemoveInList { get; private set; }
        internal List<string> FlagsRetainInList { get; private set; }
        internal bool IsBitFlagAsValue { get; private set; }
        internal bool IsSpaceAddedInText { get; private set; }
        internal bool IsSpaceAddedInValue { get; private set; }

        public EnumLookupFluentBuilder(EnumLookup lookup)
        {
            _lookup = lookup;
            FlagsToRemoveInList = new List<string>();
            FlagsRetainInList = new List<string>();
        }

        public IEnumLookupFluentBuilder<TEnum> SetFlagValueAsValueField()
        {
            IsBitFlagAsValue = true;
            return this;
        }

        public IEnumLookupFluentBuilder<TEnum> SetFlagNameAsValueField()
        {
            IsBitFlagAsValue = false;
            return this;
        }

        public IEnumLookupFluentBuilder<TEnum> SplitCamelCaseInTextField()
        {
            IsSpaceAddedInText = true;
            return this;
        }

        public IEnumLookupFluentBuilder<TEnum> SplitCamelCaseInValueField()
        {
            IsSpaceAddedInValue = true;
            return this;
        }

        public IEnumLookupFluentBuilder<TEnum> RemoveFlag(TEnum tEnum)
        {
            tEnum.FluentEnumGuard().CheckAndThrowNull().CheckAndThrowNotEnum().CheckAndThrowInvalidEnumArgument();

            var parsedValue = Enum.Parse(typeof(TEnum), tEnum.ToString()).ToString();
            if (FlagsToRemoveInList.Find(enumValue => enumValue == parsedValue) == null)
                FlagsToRemoveInList.Add(parsedValue);
            return this;
        }

        public IEnumLookupFluentBuilder<TEnum> ContainOnly(List<TEnum> tEnums)
        {
            tEnums.ForEach(enumToCheck =>
                {
                    var parsedValue = Enum.Parse(typeof(TEnum), enumToCheck.ToString()).ToString();
                    if (FlagsRetainInList.Find(enumValue => enumValue == parsedValue) == null)
                        FlagsRetainInList.Add(parsedValue);
                });
            return this;
        }

        public IEnumLookup GenerateLookupData()
        {
            _lookup.BuildLookup(this);
            return _lookup;
        }
    }
}
