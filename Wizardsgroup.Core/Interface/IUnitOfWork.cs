using System;

namespace Wizardsgroup.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {        
        void Save();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class;
    }
}
