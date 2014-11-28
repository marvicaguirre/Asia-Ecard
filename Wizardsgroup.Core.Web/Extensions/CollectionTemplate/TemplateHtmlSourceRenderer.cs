using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class TemplateHtmlSourceRenderer<TModel, TValue> : ITemplateHtmlSourceRenderer<TModel, TValue> where TValue : IEnumerable<object>
    {
        private readonly TemplateBuilderDataContainer<TModel, TValue> _templateBuilderDataContainer;

        public TemplateHtmlSourceRenderer(TemplateBuilderDataContainer<TModel, TValue> templateBuilderDataContainer)
        {
            _templateBuilderDataContainer = templateBuilderDataContainer;
        }

        public void RenderHtml()
        {
            var metadata = ModelMetadata.FromLambdaExpression(_templateBuilderDataContainer.Expression, _templateBuilderDataContainer.HtmlHelper.ViewData);
            var memberValue = (IEnumerable<object>)metadata.Model;

            if (!memberValue.Any())
                RenderForEachCollectionItem(null);
            else
                memberValue.ToList().ForEach(RenderForEachCollectionItem);

        }

        private void RenderForEachCollectionItem(object value)
        {
            if (value == null)
            {
                _templateBuilderDataContainer.HtmlHelper.RenderPartial(_templateBuilderDataContainer.PartialViewName);
            }
            else
            {
                var classType = value.GetType().Name;

                if (classType == "RuleExpressionBodyLimitViewModel" || classType == "RuleExpressionBodyConditionViewModel" ||
                    classType == "IDRemarkDataTypeBodyItemViewModel")
                {
                    // TODO: Jersey - After consulting with Marvic, the quick fix for this is a short delay to the thread.
                    // If a delay was not set at ScheduleLimitFieldSetTemplate, the PartialView gets pseudo-cached
                    // resulting in duplicate DataTypeIds hence, breaking the data on the Operator ComboBoxes...
                    Thread.Sleep(20); // Below 20ms will kill the randomizer code for Service & Benefit - RuleExpressions
                }

                _templateBuilderDataContainer.HtmlHelper.RenderPartial(_templateBuilderDataContainer.PartialViewName, value);
            }
        }
    }
}