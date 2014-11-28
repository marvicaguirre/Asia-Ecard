using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlCollectionModelIndexer<TModel> : IHtmlCollectionModelIndexer<TModel>
    {
        private HtmlFieldPrefixScope _htmlFieldPrefixScope;

        public HtmlCollectionModelIndexer(HtmlHelper<TModel> helper, Action<ITemplateIndexRegistrator> registerCollection)
        {        
            var register = new TemplateIndexRegistrator();
            registerCollection(register);
            var container = register.RegistratorContainer;
            var currentItem = container.CurrentItem;
            var parentEntries = currentItem.ContainerHolder;
            IndexFormater(helper,currentItem.Name, currentItem.IndexId, parentEntries,register.SkipIndexIdentifierId);
        }

        private void IndexFormater(HtmlHelper<TModel> helper,string name,Guid index,List<ContainerHolder> parentEntries,bool skipIndexIdentifier)
        {            
            var currentEntry = String.Format("{0}[{1}]", name, index);
            if (parentEntries.Any())
            {                
                var formatedParents = GetFormatedParentIndex(parentEntries);
                var concatenatedParentEntry = GetConcatenatedParentEntry(formatedParents);
                currentEntry = string.Format("{0}{1}", concatenatedParentEntry, currentEntry);
                name = string.Format("{0}{1}", concatenatedParentEntry, name);
            }            
            SetScopePrefixFromIndexResult(helper, name, index, currentEntry,skipIndexIdentifier);
        }

        private static string GetConcatenatedParentEntry(IEnumerable<string> formatedParents)
        {
            var concatenatedParentEntry = string.Empty;
            concatenatedParentEntry = formatedParents.Aggregate(concatenatedParentEntry,(current, entry) => current + (entry + "."));
            return concatenatedParentEntry;
        }

        private static IEnumerable<string> GetFormatedParentIndex(IEnumerable<ContainerHolder> parentEntries)
        {
            var formatedParents = new List<string>();
            parentEntries.OrderBy(o => o.Order).ToList().ForEach(entry =>
                {
                    var formatedEntry = String.Format("{0}[{1}]", entry.Name, entry.IndexId);
                    formatedParents.Add(formatedEntry);
                });
            return formatedParents;
        }

        private void SetScopePrefixFromIndexResult(HtmlHelper<TModel> helper, string itemName, Guid itemIndex, string formatedItemName, bool skipIndexIdentifier)
        {
            CreateIndexIdentifier(helper, itemName, itemIndex, skipIndexIdentifier);
            _htmlFieldPrefixScope = new HtmlFieldPrefixScope(helper.ViewData.TemplateInfo, formatedItemName);
        }

        private void CreateIndexIdentifier(HtmlHelper<TModel> helper, string itemName, Guid itemIndex, bool skipIndexIdentifier)
        {
            if (skipIndexIdentifier) return;

            var indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>
                {
                    {"name", String.Format("{0}.Index", itemName)},
                    {"value", itemIndex.ToString()},
                    {"type", "hidden"},
                    {"autocomplete", "off"}
                });

            helper.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));
        }

        public void Dispose()
        {
            _htmlFieldPrefixScope.Dispose();
        }
    }
}