using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class CardPrefixService : AbstractEntityService<CardPrefix>
    {
        public CardPrefixService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CardPrefixService(IUnitOfWork unitOfWork, IEventAggregator ea) : base(unitOfWork, ea)
        {
        }

        protected override Expression<Func<CardPrefix, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.CardPrefixId == id;
        }

        protected override Expression<Func<CardPrefix, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<CardPrefix> OrderBy(IQueryable<CardPrefix> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}
