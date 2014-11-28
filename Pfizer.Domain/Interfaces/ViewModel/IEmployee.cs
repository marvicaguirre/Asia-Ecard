using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IEmployee
    {
        int EmployeeId { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        int? CompanyId { get; set; }
        int? DepartmentId { get; set; }
        int? EmployeeTypeId { get; set; }
    }
}
