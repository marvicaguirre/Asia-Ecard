using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Repository.AuditTrail
{
    public class AuditTrailHandler
    {
        private readonly List<Type> _typesToExclude;
        private readonly IContext _context;
        private readonly List<IAuditAction> _auditActions;

        public AuditTrailHandler(List<Type> typesToExclude,IContext context)
        {
            _typesToExclude = typesToExclude;
            _context = context;
            _auditActions = new List<IAuditAction>
            {
                new AddedAuditTrail(),new DeletedAuditTrail(),new ModifiedAuditTrail()
            };
        }

        public void HandleAuditTrail(List<DbEntityEntry> entries)
        {
            var entriesToAudit = entries.Where(item => !_typesToExclude.Contains(item.GetType())).ToList();
            var entitySet = _context.EntitySet<AuditLog>();
            entriesToAudit.ForEach(item =>
            {
                var auditLogs = _auditActions.Single(logger => logger.IsSatisfiedBy(item.State)).GetAuditLogs(item);                
                auditLogs.ForEach(log => entitySet.Add(log));
            });
        }
    }
}
