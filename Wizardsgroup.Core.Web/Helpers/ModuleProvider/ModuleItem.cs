namespace Wizardsgroup.Core.Web.Helpers.ModuleProvider
{
    public class ModuleItem : IModuleItem
    {
        #region Implementation of IModuleItem

        public string ModuleName { get; set; }
        public string FunctionName { get; set; }
        public string ModuleItemText { get; set; }
        public string ControllerAction { get; set; }
        public string ControllerName { get; set; }
        public string ControllerArea { get; set; }
        public string TabCaption { get; set; }

        private string _menuTitle = "";
        public string MenuTitle
        {
            get
            {
                return _menuTitle;
            }

            set
            {
                _menuTitle = value;
            }
        }

        /// <summary>
        /// Indicates the name of the parameter
        /// </summary>
        public string ParameterKey { get; set; }

        #endregion
    }
}