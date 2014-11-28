using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Repository.AuditTrail
{
    internal class DeletedAuditTrail : AbstractAuditAction
    {
        public override bool IsSatisfiedBy(EntityState entry)
        {
            return entry == EntityState.Deleted;
        }

        public override List<AuditLog> GetAuditLogs(DbEntityEntry entry)
        {
            var result = new List<AuditLog>();
            var keyName = GetKey(entry);
            var tableName = GetTableName(entry);
            var groupdId = Guid.NewGuid();
            entry.OriginalValues.PropertyNames.ForEach(propertyName =>
            {
                var userName = UserTracker.Instance.GetUserName();
                var currentValue = entry.OriginalValues.GetValue<object>(propertyName);
                result.Add(new AuditLog
                {
                    GroupId = groupdId,
                    UserName = userName,
                    CreatedBy = userName,
                    EventDateUtc = DateTime.UtcNow,
                    EventType = "Deleted",
                    TableName = tableName,
                    RecordId = entry.OriginalValues.GetValue<object>(keyName).ToString(),
                    ColumnName = propertyName,
                    OriginalValue = currentValue == null ? null : currentValue.ToString()
                });
            });

            return result;
        }
    }
}
