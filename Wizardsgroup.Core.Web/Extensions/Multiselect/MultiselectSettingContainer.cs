namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MultiselectSettingContainer
    {
        public MultiselectSettingContainer()
        {
            Delimeter = DefaultDelimeter;
            IsFilterable = true;
            PlaceHolder = "Select an option";
            Width = DefaultWidth;
            SelectAll = true;
        }

        public string Delimeter { get; set; }
        public bool IsFilterable { get; set; }
        public string PlaceHolder { get; set; }
        public int Width { get; set; }
        public bool SelectAll { get; set; }
        internal int DefaultWidth { get { return 200; } }
        internal string DefaultDelimeter { get { return "|"; } }
    }
}