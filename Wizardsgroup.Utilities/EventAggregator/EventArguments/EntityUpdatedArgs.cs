using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntityUpdatedArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntityUpdatedArgs(TEntity entity)
        {
            Entity = entity;            
        }

        public TEntity Entity { get; private set; }
    }
}
