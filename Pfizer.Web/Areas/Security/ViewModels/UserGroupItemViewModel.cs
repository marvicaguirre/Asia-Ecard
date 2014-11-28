using System;

namespace Pfizer.Web.Areas.Security.ViewModels
{
    public class UserGroupItemViewModel
    {
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public bool IsChecked { get; set; }
    }
}