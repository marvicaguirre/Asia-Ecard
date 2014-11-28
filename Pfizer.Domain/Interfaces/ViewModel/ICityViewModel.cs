using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface ICityViewModel
    {
        Guid CityId { get; set; }
        string Code { get; set; }
        string Name { get; set; }
    }
}
