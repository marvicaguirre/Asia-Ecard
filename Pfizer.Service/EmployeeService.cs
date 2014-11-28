using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class EmployeeService : AbstractEntityService<Employee>
    {
        public EmployeeService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<Employee, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.EmployeeId == id;
        }

        protected override Expression<Func<Employee, object>>[] Include()
        {
            return new Expression<Func<Employee, object>>[] { o => o.Department, o => o.EmployeeType };
        }

        protected override IOrderedQueryable<Employee> OrderBy(IQueryable<Employee> arg)
        {
            return arg.OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ThenBy(o => o.MiddleName);
        }

        public void UpdateEmailAddress(int userId, string email)
        {
            var user = UnitOfWork.Repository<User>().Find(userId);
            if (user != null && user.EmployeeId != null && user.EmployeeId != default(int) && user.UserName != email)
            {
                var employee = Find(user.EmployeeId.GetValueOrDefault());
                if (employee != null)
                {
                    employee.Email = email;
                    Update(employee);
                    //user.UserName = email;
                    //UnitOfWork.Repository<User>().Update(user);
                    Save();
                }
            }
        }
    }
}