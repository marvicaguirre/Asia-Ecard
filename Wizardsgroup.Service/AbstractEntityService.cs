using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Utilities.EntityFluentFilter;
using Wizardsgroup.Utilities.EventAggregator;
using Wizardsgroup.Utilities.EventAggregator.EventArguments;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Service
{
    public abstract class AbstractEntityService<T> : IEntityServiceEventAggregate<T> where T : AbstractBaseModel, new()
    {
        #region Members
        private readonly List<T> _listOfTTypeInserted = new List<T>();
        private readonly List<T> _listOfTTypeUpdated = new List<T>();
        private readonly List<T> _listOfTTypeDeleted = new List<T>();
        #endregion

        #region Properties
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected IRepository<T> EntityRepository { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }
        #endregion

        #region Constructor
        protected AbstractEntityService(IUnitOfWork unitOfWork)
            : this(unitOfWork, new SimpleEventAggregator())
        {
        }

        protected AbstractEntityService(IUnitOfWork unitOfWork, IEventAggregator ea)
        {
            unitOfWork.Guard("UnitOfWork must not be null.");
            ea.Guard("EventAggregator mus not be null.");

            UnitOfWork = unitOfWork;
            EntityRepository = unitOfWork.Repository<T>();
            EventAggregator = ea;
        }
        #endregion

        #region Functions/Methods

        public virtual T Find(int id)
        {
            var entity = Filter(FindEntityPrimaryById(id)).FirstOrDefault();
            return entity;
        }

        public virtual IPagingQueryResult<T> GetPageResult(int skip, int take,Expression<Func<T, bool>> filter = null)
        {
            int totalRecord;
            var result = Query().Filter(filter).GetPage(skip, take, out totalRecord);
            var queryResult = new PagingQueryResult<T>{ Result = result,TotalRecord = totalRecord };
            return queryResult;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Query().GetResult();
        }

        public virtual IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return Query().Filter(filter).GetResult();
        }

        public IEnumerable<T> FilterActive()
        {
            return Query().FilterActive().GetResult();
        }

        public IFluentEntityFilter<T> FilterBuilder()
        {
            return new EntityFluentFilter<T>(this);
        }

        public virtual void Insert(T domainModel)
        {
            _listOfTTypeInserted.Add(domainModel);
            EventAggregator.Publish(this, new EntityCreatingArgs<T>(domainModel));
            EntityRepository.Insert(domainModel);
        }

        public virtual void Update(T domainModel)
        {
            _listOfTTypeUpdated.Add(domainModel);
            EventAggregator.Publish(this, new EntityUpdatingArgs<T>(domainModel));
            EntityRepository.Update(domainModel);
        }

        public virtual void Delete(int id)
        {
            var domainModel = Find(id);
            _listOfTTypeDeleted.Add(domainModel);
            EventAggregator.Publish(this, new EntityDeletingArgs<T>(domainModel));
            EntityRepository.Delete(id);
        }

        public void ToogleStatus(int[] ids)
        {
            foreach (var domainModel in ids.Select(id => Find(id)))
            {
                domainModel.RecordStatus = domainModel.RecordStatus.Equals(RecordStatus.Active)
                                               ? RecordStatus.Inactive
                                               : RecordStatus.Active;
                Update(domainModel);
            }
        }

        public void Save()
        {
            UnitOfWork.Save();
            var insertedThread = new Thread(() => _PublishAggregateEvents(_listOfTTypeInserted, _ExecuteSubcribeCreatedEvent));
            insertedThread.Start();

            var updatedThread = new Thread(() => _PublishAggregateEvents(_listOfTTypeUpdated, _ExecuteSubcribeUpdatedEvent));
            updatedThread.Start();
            
            var deletedThread = new Thread(() => _PublishAggregateEvents(_listOfTTypeDeleted, _ExecuteSubcribeDeletedEvent));
            deletedThread.Start();
        }
        #endregion

        #region Concrete Helpers
        protected abstract Expression<Func<T, bool>> FindEntityPrimaryById(int id);
        protected abstract Expression<Func<T, object>>[] Include();
        protected abstract IOrderedQueryable<T> OrderBy(IQueryable<T> arg);
        #endregion

        #region Private Functions/Methods

        private IRepositoryQuery<T> Query()
        {
            var query = Include() == null
                            ? EntityRepository.Query()
                            : EntityRepository.Query().Include(Include());

            return query.OrderBy(OrderBy);
        }
        private void _PublishAggregateEvents(List<T> entities, Action<T> customEventArgs)
        {
            entities.ForEach(customEventArgs.Invoke);
            entities.RemoveAll(o => o != null);
        }

        private void _ExecuteSubcribeCreatedEvent(T domainModel)
        {
            EventAggregator.Publish(this, new EntityCreatedArgs<T>(domainModel));
        }

        private void _ExecuteSubcribeUpdatedEvent(T domainModel)
        {
            EventAggregator.Publish(this, new EntityUpdatedArgs<T>(domainModel));
        }

        private void _ExecuteSubcribeDeletedEvent(T domainModel)
        {
            EventAggregator.Publish(this, new EntityDeletedArgs<T>(domainModel));
        }

        #endregion
    }
}
