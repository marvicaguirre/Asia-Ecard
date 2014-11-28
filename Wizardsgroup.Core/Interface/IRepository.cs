using System;
using System.Linq;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Interface
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll { get; }
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(object id);
        //T Find<TKey>(TKey id);
        IQueryable<T> Find(Expression<Func<T, bool>> filter);
        void Insert(T domainModel);
        void Update(T domainModel);
        void Delete(object id);
        //void Delete<TKey>(TKey id);
        IRepositoryQuery<T> Query();
    }
}
