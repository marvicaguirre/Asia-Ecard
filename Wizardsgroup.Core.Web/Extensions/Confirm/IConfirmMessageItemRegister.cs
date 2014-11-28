namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IConfirmMessageItemRegister<TModel>
    {
        IConfirmMessageSetup<TModel> CompareValueTo(string valueToCompare);
    }
}