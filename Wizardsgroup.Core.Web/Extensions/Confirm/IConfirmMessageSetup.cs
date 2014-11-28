namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IConfirmMessageSetup<TModel>
    {
        IConfirmMessageRegister<TModel> Message(string message);
    }
}