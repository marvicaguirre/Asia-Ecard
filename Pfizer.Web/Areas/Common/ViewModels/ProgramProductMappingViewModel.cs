namespace Pfizer.Web.Areas.Common.ViewModels
{
    public class ProgramProductMappingViewModel
    {
        public int ProgramProductMappingId { get; set; }
        public int ProgramId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
    }
}