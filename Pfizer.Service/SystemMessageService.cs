using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class SystemMessageService : AbstractEntityService<SystemMessage>
    {
        public SystemMessageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        protected override Expression<Func<SystemMessage, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.SystemMessageId == id;
        }

        protected override Expression<Func<SystemMessage, object>>[] Include()
        {
            return new Expression<Func<SystemMessage, object>>[] {                  
               
            };
        }

        protected override IOrderedQueryable<SystemMessage> OrderBy(IQueryable<SystemMessage> arg)
        {
            return arg.OrderBy(o => o.Code);
        }
    }
}
