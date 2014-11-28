using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Wizardsgroup.Domain.Base;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Extensions;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Interfaces;

namespace Pfizer.Repository.History
{
    internal abstract class AbstractHistoryInserter<T, THistory> : IHistoryInserter
        where T : AbstractBaseModel, new()
        where THistory : AbstractBaseModel, IHistoryAction, new()
    {
        protected readonly IContext Context;

        protected AbstractHistoryInserter(IContext context)
        {
            context.Guard("Context must not be null.");
            Context = context;
        }

        public abstract bool IsTransactionMatchHistory(string entityName);

        public virtual void CreateHistory(DbEntityEntry entityEntry)
        {
            if (!AllowInsertHistory(entityEntry)) return;

            var mappedToHistory = DefaultMapping(entityEntry);
            mappedToHistory.CreatedDate = DateTime.Now;
            mappedToHistory.ChangeMode = GetChangeMode(entityEntry.State);
            AddOtherTransactionBeforeEntityCreate(mappedToHistory, mappedToHistory.CreatedDate, entityEntry.State);
            Context.EntitySet<THistory>().Add(mappedToHistory);
            AddOtherTransactionAfterEntityCreate(mappedToHistory, mappedToHistory.CreatedDate, entityEntry.State);
        }

        protected THistory DefaultMapping(DbEntityEntry entityEntry)
        {
            var entry = GetCastedEntity(entityEntry);
            return entry.Convert<T, THistory>(MapEntityEntryToEntityHistory);
        }

        protected virtual bool AllowInsertHistory(DbEntityEntry entityEntry)
        {
            return true;
        }

        protected abstract void MapEntityEntryToEntityHistory(T arg1, THistory arg2);

        protected abstract T GetCastedEntity(DbEntityEntry entityEntry);

        protected virtual void AddOtherTransactionBeforeEntityCreate(THistory entry, DateTime historyDate, EntityState state)
        {}

        protected virtual void AddOtherTransactionAfterEntityCreate(THistory entry, DateTime historyDate, EntityState state)
        {}

        protected string GetChangeMode(EntityState state)
        {
            var changeMode = string.Empty;

            switch (state)
            {
                case EntityState.Added:
                    changeMode = ChangeModeConstant.Added;
                    break;
                case EntityState.Deleted:
                    changeMode = ChangeModeConstant.Deleted;
                    break;
                case EntityState.Modified:
                    changeMode = ChangeModeConstant.Modified;
                    break;
            }

            return changeMode;
        }
    }
}