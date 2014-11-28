using System;
using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ButtonTemplateAction : IButtonTemplateAction
    {
        public Func<ButtonSetupHelper, ButtonConfigurationContainer, List<dynamic>> GenerateAttribute { get; set; }
        public Func<ButtonSetupHelper, ButtonConfigurationContainer, List<dynamic>, string> GenerateHtml { get; set; }
    }
}