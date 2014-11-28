using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IDepartment
    {
        int DepartmentId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsSBD { get; set; }
    }
}
