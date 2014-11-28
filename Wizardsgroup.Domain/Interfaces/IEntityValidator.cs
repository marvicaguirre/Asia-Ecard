using System.Collections.Generic;
using Wizardsgroup.Domain.Containers;

namespace Wizardsgroup.Domain.Interfaces
{
    public interface IEntityValidator<TModel> where TModel : class
    {
        IEnumerable<EntityValidationResult> Validate(TModel entity);
    }
}