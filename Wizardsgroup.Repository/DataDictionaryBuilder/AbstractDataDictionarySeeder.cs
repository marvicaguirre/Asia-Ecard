using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Repository.DataDictionaryBuilder
{
    public abstract class AbstractDataDictionarySeeder : IDataDictionarySeeder
    {
        public AbstractDataDictionarySeeder()
        {
            IDataDictionaryBuilder builder = new DataDictionaryBuilder();
            SetupDictionary(builder);
            Container = builder.Container;
        }

        protected abstract void SetupDictionary(IDataDictionaryBuilder builder);

        public void Seed(IContext context)
        {
            var dbSet = context.EntitySet<DataDictionary>();
            if (Container != null)
            {
                Container.ForEach(dictionary => dbSet.AddOrUpdate(entry => new { entry.Model, entry.FieldName }, dictionary));
            }
            context.SaveChanges();
        }

        public List<DataDictionary> Container { get; private set; }
    }
}
