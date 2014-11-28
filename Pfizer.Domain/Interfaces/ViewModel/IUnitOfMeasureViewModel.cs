using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pfizer.Domain.Models;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IUnitOfMeasureViewModel
    {
        int UnitOfMeasureId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Status { get; set; }
    }
}
