using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;
using System.Collections.Generic;
using Wizardsgroup.Utilities.Security;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Service
{
    public class UserService : AbstractEntityService<User>, IUserService
    {
        #region Constructor
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        #endregion

        #region Function/Method

        protected override Expression<Func<User, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.UserId == id;
        }

        protected override Expression<Func<User, object>>[] Include()
        {
            return null;
            //return new Expression<Func<User, object>>[] {o=>o.Employee};
        }

        protected override IOrderedQueryable<User> OrderBy(IQueryable<User> arg)
        {
            return arg.OrderBy(o => o.UserName);
        }
        #endregion

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var userSalt = PasswordHelper.CreateSalt(username);
            var userHashPassword = PasswordHelper.HashPassword(userSalt, password);
            var user = FilterBuilder()
                .Filter(o => o.UserName.ToLower().Equals(username.ToLower()))
                .AndAlso(o => o.Password.Equals(userHashPassword))
                .AndAlso(o => o.RecordStatus == RecordStatus.Active)
                .ExecuteFilter().FirstOrDefault();

            return user;
        }

        public List<CentralFunctionEx> GetUserAccess(Func<IUnitOfWork> unitOfWork ,int userId)
        {
            var retVal = new ConcurrentBag<CentralFunctionEx>();

            var userGrpMapSvc = new UserGroupMapService(unitOfWork());
            var records = userGrpMapSvc.FilterBuilder()
                .Filter(c => c.UserId.Value == userId)
                .AndAlso(c => c.UserGroupId != null)
                .AndAlso(c => c.AbstractUserGroup.RecordStatus == RecordStatus.Active)
                .ExecuteFilter()
                .ToList();

            Parallel.ForEach(records, item =>
            {
                if (item.UserGroupId.HasValue)
                {
                    using (IUnitOfWork unitOfWorkInstance = unitOfWork())
                    {
                        var result = new UserGroupFunctionService(unitOfWorkInstance)
                            .GetAssignedFunctionFromGroupId(item.UserGroupId.Value)
                            .Select(x => new CentralFunctionEx
                            {
                                CentralFunctionId = x.CentralFunctionId,
                                FullFunctionName = x.FullFunctionName,
                                FunctionName = x.FunctionName,
                                ModuleName = x.ModuleName,
                                UserId = userId,
                            }).ToList();
                        result.ForEach(retVal.Add);
                    }
                }
            });
            return retVal.ToList();
        }

        public bool UserHasAccess(BasicFunction basicFunction, string moduleNameNoWhitespace, int userId)
        {
            bool hasAccess = false;

            if (moduleNameNoWhitespace != null && moduleNameNoWhitespace.Trim() != string.Empty)
            {
                string functionName = string.Format("{0}{1}",
                    basicFunction.ToString(),
                    moduleNameNoWhitespace);

                hasAccess = (from a in UnitOfWork.Repository<UserGroupMap>().GetAll
                             join b in UnitOfWork.Repository<UserGroupFunction>().GetAll on a.UserGroupId equals b.UserGroupId
                             join c in UnitOfWork.Repository<CentralFunction>().GetAll on b.CentralFunctionId equals c.CentralFunctionId
                             where c.FunctionName == functionName & a.UserId == userId
                             select c.FunctionName)
                            .Any();
            }

            return hasAccess;
        }

        public string CreateSaltPassowrd(string userName, string password)
        {
            var saltUsername = PasswordHelper.CreateSalt(userName);
            var hashPassword = PasswordHelper.HashPassword(saltUsername, password);
            return hashPassword;
        }


    }
}
