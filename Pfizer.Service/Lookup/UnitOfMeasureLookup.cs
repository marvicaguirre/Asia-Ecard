using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service.Attributes;
using Wizardsgroup.Service.Lookup;

namespace Pfizer.Service.Lookup{

    [EntityLookup(EntityLookupConstant.UnitOfMeasure)]
    public class UnitOfMeasureLookup  : AbstractLookup<UnitOfMeasure>
    {
        public UnitOfMeasureLookup(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override IEnumerable<UnitOfMeasure> GetCascadeResultHelper(int id)
        {
            return GetRecordsForLookupWorker();
        }

        protected override Expression<Func<UnitOfMeasure, string>> GetTextFieldHelper()
        {
            return o => o.Name;
        }

        protected override Expression<Func<UnitOfMeasure, int>> GetValueFieldHelper()
        {
            return o => o.UnitOfMeasureId;
        }
    }
}
