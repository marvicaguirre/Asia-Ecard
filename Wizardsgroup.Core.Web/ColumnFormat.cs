using System;

namespace Wizardsgroup.Core.Web
{
    public class ColumnFormat : IComparable<ColumnFormat>
    {
        public string field { get; set; }
        public string width { get; set; }
        public string title { get; set; }
        public string template { get; set; }
        public bool? sortable { get; set; }
        public bool? filterable { get; set; }
        public bool? groupable { get; set; }
        public bool? locked { get; set; }
        public bool? lockable { get; set; }
        public bool? menu { get; set; }
        #region Implementation of IComparable<in ColumnFormat>

        public int CompareTo(ColumnFormat other)
        {
            var ret1 = String.Compare(field, other.field, StringComparison.Ordinal);
            var ret2 = String.Compare(title, other.title, StringComparison.Ordinal);
            if (ret1 == 1 || ret2 == 1) return 1;
            if (ret1 == -1 && ret2 == -1) return -1;
            return 0;
        }

        #endregion
    }
}