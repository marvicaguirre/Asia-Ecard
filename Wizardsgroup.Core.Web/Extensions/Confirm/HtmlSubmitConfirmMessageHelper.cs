using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlSubmitConfirmMessageHelper<TModel> : IHtmlSubmitConfirmMessageHelper<TModel>
    {
        private readonly ConfirmMessageContainer<TModel> _confirmMessageContainer;

        public HtmlSubmitConfirmMessageHelper(HtmlHelper<TModel> helper, string name)
        {
            helper.Guard(string.Format("HtmlHelper<{0}> must not be null.",typeof(TModel).Name));
            name.Guard("Confirm group name message must not be null or empty.");
            _confirmMessageContainer = new ConfirmMessageContainer<TModel> { HtmlHelper = helper, Name = name };
        }

        public MvcHtmlString Register(Action<IConfirmMessageRegister<TModel>> register)
        {
            var confirmRegister = new ConfirmMessageRegister<TModel>(_confirmMessageContainer);
            register(confirmRegister);
            const string start = "var messagePrompt =''; var ConfirmOnSubmit = {Prompt:function(){";

            var itemJs = string.Empty;
            itemJs = _confirmMessageContainer.Items.Aggregate(itemJs, (current, next) => string.Format("{0}{1}{2}", current, Environment.NewLine, next.GenerateJavascript()));

            const string continueConfirmOnSubmit = "}};";
            const string end = "$(function(){confirmOnSubmitOverrideFx = function() {messagePrompt = ''; ConfirmOnSubmit.Prompt();return confirmOnSubmitOverrideFxMessage(messagePrompt)};});";
            return MvcHtmlString.Create(string.Format("<script>{0}{1}{2}{3}{4}{5}</script>", start, Environment.NewLine, itemJs, Environment.NewLine, continueConfirmOnSubmit,end));
        }
    }
}