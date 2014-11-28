using System.Collections.Generic;

namespace Wizardsgroup.Core.Interface
{
    public interface IPagingQueryResult<T>
    {
        IEnumerable<T> Result { get; set; }
        int TotalRecord { get; set; }
    }
}