using System.Linq;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.EventAggregator.EventArguments;

namespace Pfizer.Service.Subscribers
{
    public class ProductIdGenerationSubcriber : ISubscriber<EntityCreatingArgs<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductIdGenerationSubcriber(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnEvent(object sender, EntityCreatingArgs<Product> e)
        {           
        }

    }
}
