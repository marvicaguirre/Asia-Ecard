using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IRegionViewModel
    {
        Guid RegionId { get; set; }
        string Code { get; set; }
        string Name { get; set; }
    }
}
