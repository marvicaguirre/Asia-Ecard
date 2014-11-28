using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Repository.AuditTrail
{
    internal class AddedAuditTrail : AbstractAuditAction
    {
        public override bool IsSatisfiedBy(EntityState entry)
        {
            return entry == EntityState.Added;
        }

        public override List<AuditLog> GetAuditLogs(DbEntityEntry entry)
        {
            var result = new List<AuditLog>();
            var keyName = GetKey(entry);
            var tableName = GetTableName(entry);
            var groupdId = Guid.NewGuid();
            entry.CurrentValues.PropertyNames.ForEach(propertyName =>
            {
                var userName = UserTracker.Instance.GetUserName();
                var currentValue = entry.CurrentValues.GetValue<object>(propertyName);
                result.Add(new AuditLog
                {
                    GroupId = groupdId,
                    UserName = userName ?? "System",
                    CreatedBy = userName ?? "System",
                    EventDateUtc = DateTime.UtcNow,
                    EventType = "Added",
                    TableName = tableName,
                    RecordId = entry.CurrentValues.GetValue<object>(keyName).ToString(),
                    ColumnName = propertyName,
                    NewValue = currentValue == null ? null : currentValue.ToString()
                });
            });

            return result;
        }
    }
}
