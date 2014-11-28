using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IProductViewModel
    {
        int ProductId { get; set; }
        int UniqueId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
