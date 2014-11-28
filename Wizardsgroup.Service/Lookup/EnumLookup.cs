using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Domain.Lookup;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Service.Lookup
{
    public class EnumLookup : IEnumLookup
    {
        #region Members
        private readonly List<LookupData> _lookupData = new List<LookupData>();
        #endregion

        #region Public Functions
        public IEnumLookupFluentBuilder<TEnum> LookupFluentBuilder<TEnum>()
        {
            Guard<TEnum>();
            return new EnumLookupFluentBuilder<TEnum>(this);
        }

        public IEnumerable<LookupData> GetLookup()
        {
            return _lookupData;
        }
        #endregion

        #region Internal Functions
        internal void BuildLookup<TEnum>(EnumLookupFluentBuilder<TEnum> builder)
        {
            var lookups = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList()
                .FindAll(o => builder.FlagsRetainInList.Any() 
                    ? builder.FlagsRetainInList.Contains(o.ToString()) 
                    : !builder.FlagsToRemoveInList.Contains(o.ToString()))
                .Select(o => CreateLookupData(builder, o));

            _lookupData.AddRange(lookups);
        }
        #endregion

        #region Private Functions

        private void Guard<TEnum>()
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum is not enum.");
            }
        }

        private LookupData CreateLookupData<TEnum>(EnumLookupFluentBuilder<TEnum> builder, TEnum o)
        {
            return LookupData.Create(SplitCamelCaseText(builder, o.ToString()), SplitCamelCaseValue(builder, GetLookupDataValueField(builder, o)));
        }

        private string SplitCamelCaseText<TEnum>(EnumLookupFluentBuilder<TEnum> builder, string inputString)
        {
            return builder.IsSpaceAddedInText ? inputString.SplitCamelCase() : inputString;
        }

        private string SplitCamelCaseValue<TEnum>(EnumLookupFluentBuilder<TEnum> builder, string inputString)
        {
            return builder.IsSpaceAddedInValue ? inputString.SplitCamelCase() : inputString;
        }

        private string GetLookupDataValueField<TEnum>(EnumLookupFluentBuilder<TEnum> builder, TEnum o)
        {
            return builder.IsBitFlagAsValue ? EnumBitFlagValueToString(o) : o.ToString();
        }

        private string EnumBitFlagValueToString<TEnum>(TEnum o)
        {
            var result = string.Format("{0}", (int)Enum.Parse(typeof(TEnum), o.ToString()));
            return result;
        }
        #endregion
    }
}