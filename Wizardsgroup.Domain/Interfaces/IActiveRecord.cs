using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Domain.Interfaces
{
    public interface IActiveRecord
    {
        RecordStatus RecordStatus { get; set; }
    }
}