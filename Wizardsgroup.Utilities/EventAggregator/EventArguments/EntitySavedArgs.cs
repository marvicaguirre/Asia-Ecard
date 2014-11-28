using System;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator.EventArguments
{
    public class EntitySavedArgs<TEntity> : EventArgs, ICustomEntityEventArg<TEntity>
    {
        public EntitySavedArgs(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
