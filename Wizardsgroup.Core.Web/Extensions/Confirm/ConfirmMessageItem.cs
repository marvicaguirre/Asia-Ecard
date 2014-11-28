using System;
using System.Text;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ConfirmMessageItem
    {
        private const string OpenBracket = "{";
        private const string CloseBracket = "}";
        public ModelMetadata Metadata { get; set; }
        public string ValueToCompare { get; set; }
        public string Message { get; set; }
        public ConfirmForType ConfirmForType { get; set; }

        public string GenerateJavascript()
        {            
            var builder = new StringBuilder();
            var item = string.Format("var {0} = {1}Message:'{2} ',ValueToCompare:'{3}',OriginalValue:'{4}',{5};", Metadata.PropertyName, OpenBracket, Message,ValueToCompare,Metadata.Model,CloseBracket);

            var valueOfItem = ConfirmForType.ComboBox  == ConfirmForType ?  "$('#" + Metadata.PropertyName + "').data('kendoComboBox').text()" 
                : "$('#" + Metadata.PropertyName + "').is(\":checked\")";
            valueOfItem = "var itemCurrent_" + Metadata.PropertyName + "=" + valueOfItem;
            var itemValueName = "itemCurrent_" + Metadata.PropertyName;

            var valueOfItem2 = ConfirmForType.ComboBox == ConfirmForType ? "$('#" + Metadata.PropertyName + "').val()"
                : "$('#" + Metadata.PropertyName + "').is(\":checked\")";
            valueOfItem2 = "var itemCurrent_" + Metadata.PropertyName + "2=" + valueOfItem2;
            var itemValueName2 = "itemCurrent_" + Metadata.PropertyName + "2";

            var compareValue = "if(" + Metadata.PropertyName + ".OriginalValue != " + itemValueName2 + " && " + Metadata.PropertyName + ".ValueToCompare == " + itemValueName + ")" +
                               "{console.log(" + Metadata.PropertyName + ".OriginalValue); console.log(" + Metadata.PropertyName + ".ValueToCompare); console.log(" + Metadata.PropertyName + ".Message);" +
                               "messagePrompt = messagePrompt + " + Metadata.PropertyName + ".Message;};";

            builder.Append(item + Environment.NewLine);
            builder.Append(valueOfItem + Environment.NewLine);
            builder.Append(valueOfItem2 + Environment.NewLine);
            builder.Append(compareValue);
            return builder.ToString();
        }
    }
}

