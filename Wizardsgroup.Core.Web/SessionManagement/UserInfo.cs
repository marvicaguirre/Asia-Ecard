using System;
using System.Collections.Generic;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Core.Web.SessionManagement
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(AbstractEmployee employee)
        {
            Employee = employee;
        }
        //public Employee Employee { get; private set; }
        //public Company Company { get; private set; }
        public List<Guid> Roles { get; private set; }
        public AbstractEmployee Employee { get; private set; }
    }
}