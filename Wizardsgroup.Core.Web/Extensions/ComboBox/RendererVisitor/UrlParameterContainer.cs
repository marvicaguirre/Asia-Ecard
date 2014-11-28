namespace Wizardsgroup.Core.Web.Extensions
{
    internal class UrlParameterContainer
    {
        private readonly string[] _parameter;

        public UrlParameterContainer(string url, params string[] parameter)
        {
            _parameter = parameter;
            Url = url;
        }

        public string Url { get; private set; }
        public string Parameter { get { return string.Join(",", _parameter); } }
    }
}