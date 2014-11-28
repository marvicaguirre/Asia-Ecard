using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IEntityService<T>
    {
        T Find(int id);
        IPagingQueryResult<T> GetPageResult(int skip, int take, Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Expression<Func<T, bool>> filter);
        IEnumerable<T> FilterActive();
        IFluentEntityFilter<T> FilterBuilder();
        void Insert(T domainModel);
        void Update(T domainModel);
        void Delete(int id);
        void ToogleStatus(int[] ids);
        void Save();
    }
}
