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
    public class DosageService : AbstractEntityService<Dosage>
    {
        public DosageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<Dosage, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.DosageId.Equals(id);
        }

        protected override Expression<Func<Dosage, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<Dosage> OrderBy(IQueryable<Dosage> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}
