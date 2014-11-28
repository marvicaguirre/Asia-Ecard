using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntityDeletingArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntityDeletingArgs(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
