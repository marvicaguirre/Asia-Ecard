using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Repository.AuditTrail
{
    internal class ModifiedAuditTrail : AbstractAuditAction
    {
        public override bool IsSatisfiedBy(EntityState entry)
        {
            return entry == EntityState.Modified;
        }

        public override List<AuditLog> GetAuditLogs(DbEntityEntry entry)
        {
            var result = new List<AuditLog>();
            var keyName = GetKey(entry);
            var tableName = GetTableName(entry);
            var groupdId = Guid.NewGuid();
            entry.CurrentValues.PropertyNames.ForEach(propertyName =>
            {
                var original = entry.GetDatabaseValues().GetValue<object>(propertyName);//entry.OriginalValues.GetValue<object>(propertyName);
                var current = entry.CurrentValues.GetValue<object>(propertyName);

                if (Equals(original, current)) return;

                var userName = UserTracker.Instance.GetUserName();

                result.Add(new AuditLog
                {
                    GroupId = groupdId,
                    UserName = userName,
                    CreatedBy = userName,
                    EventDateUtc = DateTime.UtcNow,
                    EventType = "Modified",
                    TableName = tableName,
                    RecordId = entry.OriginalValues.GetValue<object>(keyName).ToString(),
                    ColumnName = propertyName,
                    OriginalValue = original == null ? null : original.ToString(),
                    NewValue = current == null ? null : current.ToString()
                });
            });

            return result;
        }
    }
}
