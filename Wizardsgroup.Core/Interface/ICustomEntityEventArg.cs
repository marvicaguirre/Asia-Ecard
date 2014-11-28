namespace Wizardsgroup.Core.Interface
{
    public interface ICustomEntityEventArg<TEntity>
    {        
        TEntity Entity { get; }
    }
}
