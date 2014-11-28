using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : AbstractBaseModel, new()
    {
        #region Members
        internal readonly IContext Context;
        internal IDbSet<TEntity> DbSet;
        #endregion

        #region Constructor
        public Repository(IContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Context = context;
            DbSet = Context.EntitySet<TEntity>();
        }
        #endregion

        #region Function/Methods
        public IQueryable<TEntity> GetAll
        {
            get { return DbSet; }
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(GetAll, (current, includeProperty) => current.Include(includeProperty));
        }

        public TEntity Find(object id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(filter);
        }

        public void Insert(TEntity domainModel)
        {
            domainModel.CreatedDate = DateTime.Now;
            Context.SetAddState(domainModel);
        }

        public void Update(TEntity domainModel)
        {
            domainModel.ModifiedDate = DateTime.Now;
            Context.SetModifiedState(domainModel);
        }

        public void Delete(object id)
        {
            var entity = Find(id);
            Context.SetDeletedModified(entity);
        }

        public virtual IRepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);
            return repositoryGetFluentHelper;
        }

        internal IQueryable<TEntity> Get(List<Expression<Func<TEntity, bool>>> filters = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(prop => { query = query.Include(prop); });

            if (filters != null)
                filters.ForEach(filter => { query = query.Where(filter); });

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null && take != null)
                query = query
                    .Skip(skip.Value)
                    .Take(take.Value);

            var results = query;

            return results;
        }
        #endregion

    }
}
