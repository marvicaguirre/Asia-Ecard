using System;
using System.Collections.Generic;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Utilities.Security;

namespace Pfizer.Service
{
    public interface IUserService
    {
        User GetUserByUsernameAndPassword(string username, string password);
        List<CentralFunctionEx> GetUserAccess(Func<IUnitOfWork> unitOfWork ,int userId);
        bool UserHasAccess(BasicFunction basicFunction, string moduleNameNoWhitespace, int userId);
        string CreateSaltPassowrd(string userName, string password);
    }
}