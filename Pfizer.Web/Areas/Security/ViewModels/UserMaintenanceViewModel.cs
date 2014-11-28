using System;
using System.Collections.Generic;

namespace Pfizer.Web.Areas.Security.ViewModels
{
    public class UserMaintenanceViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] UserGroupIds { get; set; }
        public List<UserGroupItemViewModel> GroupIdList { get; set; } 
        public List<UserGroupItemViewModel> UserGroupList { get; set; }
    }
}