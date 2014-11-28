namespace Wizardsgroup.Core.Web.Helpers.HomePageHelper
{
    public class HomePageItem : IHomePageItem
    {
        #region Implementation of IHomePageItem

        public string ModuleName { get; set; }
        public string FunctionName { get; set; }
        public string ModuleItemText { get; set; }
        public string ControllerAction { get; set; }
        public string ControllerName { get; set; }
        public string ControllerArea { get; set; }
        public string TabCaption { get; set; }
        public string ParameterKey { get; set; }
        public string MenuTitle { get; set; }

        #endregion
    }
}