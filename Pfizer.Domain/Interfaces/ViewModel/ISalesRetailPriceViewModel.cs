using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pfizer.Domain.Models;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface ISalesRetailPriceViewModel
    {
        int SalesRetailPriceId { get; set; }
        int PriceTypeId { get; set; }
        //TODO: For PriceType
        string PriceTypeName { get; set; }
        DateTime? From { get; set; }
        DateTime? To { get; set; }
        decimal Price { get; set; }
        int ProductId { get; set; }
        string ProductName { get; set; }
        int DosageId { get; set; }
        string DosageForm { get; set; }
        string ConversionPerUnit { get; set; }
    }
}
