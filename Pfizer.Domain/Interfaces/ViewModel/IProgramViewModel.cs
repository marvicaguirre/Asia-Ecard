namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface IProgramViewModel
    {
        int ProgramId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string VendorCode { get; set; }
        int CardTypeId { get; set; }
    }
}
