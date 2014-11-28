using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IConversionFactorViewModel
    {
        int ConversionFactorId { get; set; }
        string PfizerCode { get; set; }
        int UnitOfMeasureId { get; set; }
        int DosageId { get; set; }
        string ProductName { get; set; }
        string DosageForm { get; set; }
        string UnitOfMeasureName { get; set; }
        decimal Factor { get; set; }
    }
}
