using System;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;

namespace Pfizer.Service
{
    public class CompanyService : AbstractEntityService<Company>
    {
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CompanyService(IUnitOfWork unitOfWork, IEventAggregator ea) : base(unitOfWork, ea)
        {
        }

        protected override Expression<Func<Company, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.CompanyId == id;
        }

        protected override Expression<Func<Company, object>>[] Include()
        {
            return new Expression<Func<Company, object>>[]
                {
                    o=>o.CompanyClassification
                };
        }

        protected override IOrderedQueryable<Company> OrderBy(IQueryable<Company> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}
