namespace Wizardsgroup.Core.Interface
{
    public interface IEntityServiceEventAggregate<T> : IEntityService<T>
    {
        IEventAggregator EventAggregator { get; }
    }
}