using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Repository.AuditTrail
{
    internal abstract class AbstractAuditAction : IAuditAction
    {
        public abstract bool IsSatisfiedBy(EntityState entry);
        public abstract List<AuditLog> GetAuditLogs(DbEntityEntry entry);

        protected string GetKey(DbEntityEntry entry)
        {
            string key =entry.Entity
                .GetType()
                .GetProperties()
                .Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any())
                .Name;

            return key;
        }

        protected string GetTableName(DbEntityEntry entry)
        {
            string tableName = entry.Entity.GetType().Name;
            return tableName;
        }
    }
}
