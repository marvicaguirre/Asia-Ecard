using System.Data.Entity.Infrastructure;

namespace Pfizer.Repository.History
{
    public interface IHistoryInserter
    {
        bool IsTransactionMatchHistory(string entityName);
        void CreateHistory(DbEntityEntry entityEntry);
    }
}