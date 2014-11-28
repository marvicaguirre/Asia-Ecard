using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IUser
    {
        int UserId { get; set; }
        int? EmployeeId { get; set; }
        string UserName { get; set; }
        string UserPassword { get; set; }
    }
}
