using System;
using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal interface IButtonTemplateAction
    {
        Func<ButtonSetupHelper, ButtonConfigurationContainer, List<dynamic>> GenerateAttribute { get; set; }
        Func<ButtonSetupHelper, ButtonConfigurationContainer, List<dynamic>, string> GenerateHtml { get; set; }
    }
}