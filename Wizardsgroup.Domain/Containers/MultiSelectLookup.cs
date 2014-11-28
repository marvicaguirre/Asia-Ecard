using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Domain.Containers
{
    public class MultiSelectLookup : IMultiSelectLookupValueField
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }

        #region Constructor
        public MultiSelectLookup()
        {
        }
        public MultiSelectLookup(string text, string value)
        {
            Text = text;
            Value = value;
            //Code = code;
        }
        #endregion

        public static MultiSelectLookup Create(string text, string value)
        {
            return new MultiSelectLookup(text, value);
        }
    }
}
