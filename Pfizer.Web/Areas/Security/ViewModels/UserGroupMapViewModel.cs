using System;
using Pfizer.Domain.Interfaces.ViewModel;

namespace Pfizer.Web.Areas.Security.ViewModels
{
    public class UserGroupMapViewModel : IUserGroupMap
    {
        public int UserGroupId { get; set; }
        public int UserGroupMapId { get; set; }
        public string UserGroupName { get; set; }
        public string UserGroupDesc { get; set; }
    }
}