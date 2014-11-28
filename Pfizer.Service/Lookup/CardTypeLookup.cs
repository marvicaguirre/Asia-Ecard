using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service.Attributes;
using Wizardsgroup.Service.Lookup;

namespace Pfizer.Service.Lookup
{
    [EntityLookup(EntityLookupConstant.CardType)]
    public class CardTypeLookup : AbstractLookup<CardType>
    {
        public CardTypeLookup(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IEnumerable<CardType> GetCascadeResultHelper(int id)
        {
            return GetRecordsForLookupWorker();
        }

        protected override Expression<Func<CardType, string>> GetTextFieldHelper()
        {
            return o => o.Name;
        }

        protected override Expression<Func<CardType, int>> GetValueFieldHelper()
        {
            return o => o.CardTypeId;
        }
    }
}
