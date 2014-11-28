using System;
using System.Collections;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members
        private readonly IContext _context;
        private Hashtable _repositories;
        private bool _disposed; 
        private object _lockedObject = new object();
        #endregion

        #region Constructor
        public UnitOfWork(IContext context)
        {
            _context = context;
        } 
        #endregion

        #region Function/Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                lock (_lockedObject)
                {
                    _repositories.Add(type, repositoryInstance);    
                }                
            }

            return (IRepository<T>)_repositories[type];
        }

        #endregion
    }
}
