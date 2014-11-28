namespace Wizardsgroup.Core.Web.Extensions
{
    public class ModalProperyContainer
    {
        public ModalProperyContainer()
        {
            AutoClose = true;
            ConfirmWindow = true;
        }
        public string Title { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool AutoClose { get; set; }
        public bool ConfirmWindow { get; set; }
    }
}