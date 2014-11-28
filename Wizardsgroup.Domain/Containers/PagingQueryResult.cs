using System.Collections.Generic;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Domain.Containers
{
    public class PagingQueryResult<T> : IPagingQueryResult<T>
    {
        public IEnumerable<T> Result { get; set; }
        public int TotalRecord { get; set; }
        
    }
}
