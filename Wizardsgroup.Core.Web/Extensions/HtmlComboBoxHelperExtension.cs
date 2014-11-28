using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using UrlHelper = System.Web.Mvc.UrlHelper;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlComboBoxHelperExtension
    {
        private const string PlaceHolder = "Select A Record";

        public static MvcHtmlString CustomDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityTypeLookup, object htmlAttributes = null, string placeHolderString = "", string index = "")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            string fieldId = metadata.PropertyName;

            var attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes["service"] = entityTypeLookup;
            attributes["controltype"] = "kendoDropDownList";
            attributes["serverFiltering"] = false.ToString();
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["id"] = fieldId + index;
            attributes["name"] = fieldId;
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();

            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityTypeLookup, object htmlAttributes = null, string placeHolderString = "", string index = "")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            string fieldId = metadata.PropertyName;

            var attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes["service"] = entityTypeLookup;
            attributes["controltype"] = "kendoComboBox";
            attributes["serverFiltering"] = false.ToString();
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["id"] = fieldId + index;
            attributes["name"] = fieldId;            
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();            

            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomReadOnlyComboBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityTypeLookup, object htmlAttributes = null, string placeHolderString = "")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            string fieldId = metadata.PropertyName;

            var attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes["service"] = entityTypeLookup;
            attributes["controltype"] = "kendoComboBoxReadOnly";
            attributes["serverFiltering"] = false.ToString();
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();
            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboBoxWithCascadeFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityTypeLookup, string customActionCascade, string controller, object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(customActionCascade, controller);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            string fieldId = metadata.PropertyName;

            var attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes["service"] = entityTypeLookup;
            attributes["controltype"] = "kendoComboBoxWithCascade";
            attributes["serverFiltering"] = false.ToString();
            attributes["placeHolder"] = PlaceHolder;
            attributes["customType"] = "text";
            attributes["urlCascade"] = url;
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();
            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboxBoxLinkedWithCascadeFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityTypeLookup, Expression<Func<TModel, TValue>> cascadeFromExpression, string customActionCascade, string controller, object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(customActionCascade, controller);

            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;

            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string cascadeFromValue = cascadeFromMetadata.Model == null ? string.Empty : cascadeFromMetadata.Model.ToString();
            cascadeFromValue = Convert.ToInt32(cascadeFromValue) > 0 ? cascadeFromValue : string.Empty;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            //if (cascadeFromValue == Guid.Empty.ToString()) value = string.Empty;

            string fieldId = metadata.PropertyName;
            string cascadeFrom = cascadeFromMetadata.PropertyName;

            var attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes["service"] = entityTypeLookup;
            attributes["controltype"] = "kendoComboBoxLinkedWithCascade";
            attributes["serverFiltering"] = false;
            attributes["placeHolder"] = PlaceHolder;
            attributes["customType"] = "text";
            attributes["cascadeFrom"] = cascadeFrom;
            attributes["cascadeFromValue"] = cascadeFromValue;
            attributes["urlCascade"] = url;
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();

            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboxBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityTypeLookup, Expression<Func<TModel, TValue>> cascadeFromExpression, object htmlAttributes = null, string placeHolderString = "", string index = "")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;

            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string cascadeFromValue = cascadeFromMetadata.Model == null ? string.Empty : cascadeFromMetadata.Model.ToString();
            cascadeFromValue = Convert.ToInt32(cascadeFromValue) > 0 ? cascadeFromValue : string.Empty;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            //if (cascadeFromValue == Guid.Empty.ToString()) value  = string.Empty;

            string fieldId = metadata.PropertyName;
            string cascadeFrom = cascadeFromMetadata.PropertyName;

            var attributes = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            attributes["service"] = entityTypeLookup;
            attributes["controltype"] = "kendoComboBoxLinked";
            attributes["serverFiltering"] = false;
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["cascadeFrom"] = cascadeFrom + index;
            attributes["cascadeFromValue"] = cascadeFromValue;
            attributes["id"] = fieldId + index;
            attributes["name"] = fieldId;

            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();
            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboBoxWithCustomDataFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string action, string controller, string placeHolderString = "", params string[] paramControls)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            string fieldId = metadata.PropertyName;
            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;

            var attributes = new RouteValueDictionary();
            attributes["controltype"] = "kendoComboBoxForCustomData";
            attributes["serverFiltering"] = false.ToString();
            attributes["url"] = url;
            attributes["placeHolder"] = placeHolderString.Equals(string.Empty) ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["parameter"] = string.Join(",", paramControls);
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;

            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();

            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboBoxWithCustomDataLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string action, string controller, Expression<Func<TModel, int>> cascadeFromExpression, string placeHolderString = "")
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            string fieldId = metadata.PropertyName;

            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string cascadeFromValue = cascadeFromMetadata.Model == null ? string.Empty : cascadeFromMetadata.Model.ToString();
            cascadeFromValue = Convert.ToInt32(cascadeFromValue) > 0 ? cascadeFromValue : string.Empty;
            string cascadeFrom = cascadeFromMetadata.PropertyName;

            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            //if (cascadeFromValue == Guid.Empty.ToString()) value = string.Empty;

            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;

            var attributes = new RouteValueDictionary();
            attributes["controltype"] = "kendoComboBoxForCustomDataLinkedFor";
            attributes["serverFiltering"] = false.ToString();
            attributes["url"] = url;
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["cascadeFrom"] = cascadeFrom;
            attributes["parameter"] = cascadeFrom;
            attributes["cascadeFromValue"] = cascadeFromValue;
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;

            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();
            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboBoxWithCustomDataLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string action, string controller, Expression<Func<TModel, int?>> cascadeFromExpression, string placeHolderString = "")
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            value = Convert.ToInt32(value) > 0 ? value : string.Empty;
            string fieldId = metadata.PropertyName;

            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string cascadeFromValue = cascadeFromMetadata.Model == null ? string.Empty : cascadeFromMetadata.Model.ToString();
            cascadeFromValue = Convert.ToInt32(cascadeFromValue) > 0 ? cascadeFromValue : string.Empty;
            string cascadeFrom = cascadeFromMetadata.PropertyName;

            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            //if (cascadeFromValue == Guid.Empty.ToString()) value = string.Empty;

            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;

            var attributes = new RouteValueDictionary();
            attributes["controltype"] = "kendoComboBoxForCustomDataLinkedFor";
            attributes["serverFiltering"] = false.ToString();
            attributes["url"] = url;
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["cascadeFrom"] = cascadeFrom;
            attributes["parameter"] = cascadeFrom;
            attributes["cascadeFromValue"] = cascadeFromValue;
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();
            return new MvcHtmlString(combox);
        }

        public static MvcHtmlString CustomComboBoxWithCustomDataLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string action, string controller, Expression<Func<TModel, int?>> cascadeFromExpression, string placeHolderString = "", params string[] paramControls)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            string fieldId = metadata.PropertyName;

            var cascadeFromMetadata = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string cascadeFromValue = cascadeFromMetadata.Model == null ? string.Empty : cascadeFromMetadata.Model.ToString();
            cascadeFromValue = Convert.ToInt32(cascadeFromValue) > 0 ? cascadeFromValue : string.Empty;
            string cascadeFrom = cascadeFromMetadata.PropertyName;

            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;
            //if (cascadeFromValue == Guid.Empty.ToString()) value = string.Empty;

            //Note Temporary Fixed For not nullable guid.
            //if (value == Guid.Empty.ToString()) value = string.Empty;

            var attributes = new RouteValueDictionary();
            attributes["controltype"] = "kendoComboBoxForCustomDataLinkedFor";
            attributes["serverFiltering"] = false.ToString();
            attributes["url"] = url;
            attributes["placeHolder"] = placeHolderString == "" ? PlaceHolder : placeHolderString;
            attributes["customType"] = "text";
            attributes["cascadeFrom"] = cascadeFrom;
            attributes["parameter"] = cascadeFrom + "," + string.Join(",", paramControls);
            attributes["cascadeFromValue"] = cascadeFromValue;
            attributes["id"] = fieldId;
            attributes["name"] = fieldId;
            var combox = helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes).ToString();
            return new MvcHtmlString(combox);
        }
    }
}