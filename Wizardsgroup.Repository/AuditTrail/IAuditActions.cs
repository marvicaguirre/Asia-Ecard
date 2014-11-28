using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Repository.AuditTrail
{
    public interface IAuditAction
    {
        bool IsSatisfiedBy(EntityState entry);
        List<AuditLog> GetAuditLogs(DbEntityEntry entry);
    }
}
