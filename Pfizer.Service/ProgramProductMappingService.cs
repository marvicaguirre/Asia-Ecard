using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Service;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Service
{
    public class ProgramProductMappingService : AbstractEntityService<ProgramProductMapping>, IProgramProductMappingService
    {
        private readonly IEntityService<Product> _productService;

        public ProgramProductMappingService(IUnitOfWork unitOfWork,IEntityService<Product> productService) : base(unitOfWork)
        {
            _productService = productService;
        }

        public ProgramProductMappingService(IUnitOfWork unitOfWork, IEventAggregator ea) : base(unitOfWork, ea)
        {
        }

        protected override Expression<Func<ProgramProductMapping, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.ProgramProductMappingId == id;
        }

        protected override Expression<Func<ProgramProductMapping, object>>[] Include()
        {
            return new Expression<Func<ProgramProductMapping, object>>[]
            {
                o=>o.Product,o=>o.Program
            };
        }

        protected override IOrderedQueryable<ProgramProductMapping> OrderBy(IQueryable<ProgramProductMapping> arg)
        {
            return arg.OrderBy(o=>o.Product.Name);
        }

        public IEnumerable<Product> GetAssignedProductFromProgramId(int programId)
        {
            var ids = EntityRepository.GetAll.Where(o => o.ProgramId == programId).Select(o => o.ProductId);
            return _productService.Filter(o => ids.Contains(o.ProductId));
        }

        public IEnumerable<Product> GetUnassignedProductFromProgramId()
        {
            var ids = EntityRepository.GetAll.Select(o => o.ProductId);
            return _productService.FilterBuilder()
                .Filter(o => !ids.Contains(o.ProductId))
                .AndAlso(o => o.RecordStatus == RecordStatus.Active)
                .ExecuteFilter().ToList();
        }

        public void AssignProductToProgram(int programId, int[] productIds,string userName)
        {
            if (productIds == null) productIds = new int[] { };

            var mappingIds = EntityRepository.GetAll.Where(o => o.ProgramId == programId).Select(o => new
            {
                o.ProductId,o.ProgramProductMappingId,
            });
            
            var mappingToRemove = mappingIds.Where(map => !productIds.Contains(map.ProductId));
            mappingToRemove.ForEach(map=>Delete(map.ProgramProductMappingId));

            var mappingToAdd = productIds.Where(id => !mappingIds.Select(o=>o.ProductId).Contains(id));
            mappingToAdd.ForEach(id => Insert(new ProgramProductMapping
            {
                ProgramId = programId,
                ProductId = id,
                CreatedBy = userName
            }));
            Save();
        }
    }
}
