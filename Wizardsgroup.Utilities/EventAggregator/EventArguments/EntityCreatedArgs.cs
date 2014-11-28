using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntityCreatedArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntityCreatedArgs(TEntity entity)
        {            
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
