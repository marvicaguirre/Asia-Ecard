using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntityDeletedArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntityDeletedArgs(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
