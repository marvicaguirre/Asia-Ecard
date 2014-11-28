using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface ICardTypeViewModel
    {
        int CardTypeId { get; set; }
        int ClassId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
