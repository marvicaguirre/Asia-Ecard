using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IUserGroupMap
    {
        int UserGroupId { get; set; }
        int UserGroupMapId { get; set; }
        string UserGroupName { get; set; }
        string UserGroupDesc { get; set; }
    }
}
