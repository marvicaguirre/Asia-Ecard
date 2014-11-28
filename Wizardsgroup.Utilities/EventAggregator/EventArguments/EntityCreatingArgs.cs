using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntityCreatingArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntityCreatingArgs(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
