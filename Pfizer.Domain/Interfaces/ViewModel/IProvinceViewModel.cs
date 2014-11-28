using System;


namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IProvinceViewModel
    {
        Guid ProvinceId { get; set; }
        Guid RegionId { get; set; }
        string Code { get; set; }
        string Name { get; set; }
    }
}
