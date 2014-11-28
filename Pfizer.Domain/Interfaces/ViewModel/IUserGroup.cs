using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IUserGroup
    {
        int UserGroupId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
