using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlFieldPrefixScope : IDisposable
    {
        private readonly TemplateInfo _templateInfo;
        private readonly string _previousPrefix;

        public HtmlFieldPrefixScope(TemplateInfo templateInfo, string collectionItemName)
        {
            _templateInfo = templateInfo;

            _previousPrefix = templateInfo.HtmlFieldPrefix;
            templateInfo.HtmlFieldPrefix = collectionItemName; 
        }

        public void Dispose()
        {
            _templateInfo.HtmlFieldPrefix = _previousPrefix;
        }
    }

}