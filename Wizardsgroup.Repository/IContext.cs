using System;
using System.Data.Entity;

namespace Wizardsgroup.Repository
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
        IDbSet<T> EntitySet<T>() where T : class;
        void SetAddState<T>(T entity) where T : class;
        void SetModifiedState<T>(T entity) where T : class;
        void SetDeletedModified<T>(T entity) where T : class;
    }
}
