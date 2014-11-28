using System;
using Pfizer.Domain.Models;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IDosageViewModel
    {
        int DosageId { get; set; }
        string UniqueId { get; set; }
        int ProductId { get; set; }
        string Name { get; set; }
    }
}
