using System.Collections.Generic;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;

namespace Pfizer.Service
{
    public interface IProgramProductMappingService : IEntityService<ProgramProductMapping>
    {
        IEnumerable<Product> GetAssignedProductFromProgramId(int programId);
        IEnumerable<Product> GetUnassignedProductFromProgramId();
        void AssignProductToProgram(int programId, int[] productIds, string userName);
    }
}