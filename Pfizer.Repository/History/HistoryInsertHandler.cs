using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Repository.History
{
    internal class HistoryInsertHandler
    {
        private readonly List<DbEntityEntry> _entries;
        private readonly IContext _context;

        public HistoryInsertHandler(List<DbEntityEntry> entries,IContext context)
        {
            _entries = entries;
            _context = context;
        }

        public void HandleInsertHistory()
        {
            var historyFactory = new HistoryFactory(ReflectionHelper.Instance, "Pfizer.Repository");
            var historyInserters = historyFactory.CreateHistoryInserter(_context);

            _entries.ForEach(entry =>
                {
                    string tableName = entry.Entity.GetType().Name;

                    // if table name has an underscore, it must be a proxy entity and should only retain the table name
                    // without the generated proxy name suffix
                    if (tableName.Contains('_'))
                        tableName = tableName.Split('_')[0];

                    historyInserters.FindAll(inserter => inserter.IsTransactionMatchHistory(tableName))
                        .ForEach(inserter => inserter.CreateHistory(entry));
                });
        }
    }
}
