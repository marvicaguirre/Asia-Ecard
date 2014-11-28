using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntityUpdatingArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntityUpdatingArgs(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
