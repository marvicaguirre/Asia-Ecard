using System.Linq;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;

namespace Pfizer.Service.Helper
{
    public class SystemMessageHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _defaultMessage = 
            "Warning: System message does exist. Add an entry in the SystemMessageConstant class and SystemMessageSeeder seeder.";

        public SystemMessageHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetMessage(string systemMessageConstant)
        {
            string message = _defaultMessage;
            string systemMessage = _unitOfWork.Repository<SystemMessage>()
                .Find(o => o.Code.Equals(systemMessageConstant))
                .Select(o => o.Message)
                .FirstOrDefault();

            if (systemMessage != null)
                message = systemMessage;

            return message;
        }
    }
}
