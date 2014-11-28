using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class CardTypeService : AbstractEntityService<CardType>
    {
        public CardTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<CardType, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.CardTypeId.Equals(id);
        }

        protected override System.Linq.Expressions.Expression<Func<CardType, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<CardType> OrderBy(IQueryable<CardType> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}
