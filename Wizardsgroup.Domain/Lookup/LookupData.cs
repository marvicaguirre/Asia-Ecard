using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Domain.Lookup
{
    public class LookupData : ILookupValueField
    {
        #region Properties
        public string Text { get; set; }
        public string Value { get; set; }
        //public string Code { get; set; }
        #endregion

        #region Constructor
        public LookupData()
        {
        }
        public LookupData(string text, string value)
        {
            Text = text;
            Value = value;
            //Code = code;
        }
        #endregion

        public static LookupData Create(string text, string value)
        {
            return new LookupData(text,value);
        }
    }
}
