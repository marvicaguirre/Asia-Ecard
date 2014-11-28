using System;
using System.Linq;

namespace Wizardsgroup.Core.Interface
{
    public interface IRepositoryOrderBy<T>
    {
        IRepositoryQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
    }
}