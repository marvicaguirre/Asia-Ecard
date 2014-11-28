using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntitySavingArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntitySavingArgs(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
