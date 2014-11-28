using System;

namespace Wizardsgroup.Domain.Enumerations
{    
    [Flags]
    public enum RecordStatus
    {
        Active = 0,
        Inactive = 1
    }
}