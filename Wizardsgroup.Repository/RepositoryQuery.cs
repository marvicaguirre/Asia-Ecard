using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository
{
    public sealed class RepositoryQuery<TEntity> : IRepositoryQuery<TEntity> where TEntity : AbstractBaseModel, new()
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeProperties;
        private readonly Repository<TEntity> _repository;
        private readonly List<Expression<Func<TEntity, bool>>> _filters;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderByQuerable;        

        public RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<TEntity, object>>>();
            _filters = new List<Expression<Func<TEntity, bool>>>();
        }

        public IRepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null) _filters.Add(filter);
            return this;
        }

        public IRepositoryQuery<TEntity> FilterActive()
        {
            _filters.Add(filter=>filter.RecordStatus == RecordStatus.Active);
            return this;
        }

        public IRepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>>[] expression)
        {
            foreach (var exp in expression)
            {
                _includeProperties.Add(exp);    
            }            
            return this;
        }

        public IRepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IRepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public IQueryable<TEntity> GetResult()
        {
            return _repository.Get(_filters, _includeProperties, _orderByQuerable);
        }

        public IQueryable<TEntity> GetPage(int skip, int take, out int totalCount)
        {
            totalCount = _repository.Get(_filters).Count();

            return _repository.Get(
                _filters,
                _includeProperties, 
                _orderByQuerable
                , skip, take);
        }
    }
}
