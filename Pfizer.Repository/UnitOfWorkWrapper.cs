using Wizardsgroup.Core.Interface;
using Wizardsgroup.Repository;
using Pfizer.Repository.Context;

namespace Pfizer.Repository
{
    public class UnitOfWorkWrapper : IUnitOfWork
    {
        private readonly IUnitOfWork _unitOfWorkBase;

        private UnitOfWorkWrapper(IContext context) 
        {
            _unitOfWorkBase = new UnitOfWork(context);
        }

        public UnitOfWorkWrapper() : this(new MainContext())
        {
            
        }

        public void Dispose()
        {
            _unitOfWorkBase.Dispose();
        }

        public void Save()
        {
            _unitOfWorkBase.Save();
        }

        public void Dispose(bool disposing)
        {
            _unitOfWorkBase.Dispose(disposing);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return _unitOfWorkBase.Repository<T>();
        }
    }
}
