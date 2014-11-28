using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository.Factories;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Core.Web.Helpers
{
    public sealed class DataDictionaryHelper
    {
        private DataDictionaryHelper()
        {
            _dataDictionaries = new List<DataDictionary>();
        }
        private readonly List<DataDictionary> _dataDictionaries;

        public static DataDictionaryHelper Instance
        {
            get { return Singleton<DataDictionaryHelper>.Instance; }
        }

        public bool IsRequiredField<TModel, TValue>(string dictionaryAssemblyName, HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> fieldExpression)
        {
            if (!_dataDictionaries.Any())
            {
                var dataDictionaryFactory = new DataDictionaryFactory(ReflectionHelper.Instance, dictionaryAssemblyName);
                var dictionaries = dataDictionaryFactory.CreateDataDictionarySeederDataBuilder();
                dictionaries.ForEach(o => _dataDictionaries.AddRange(o.Container));    
            }

            var metadata = ModelMetadata.FromLambdaExpression(fieldExpression, htmlHelper.ViewData);            
            var dataDictionary = _dataDictionaries.FirstOrDefault(o => o.Model.ToLower() == metadata.ContainerType.Name.ToLower() 
                && o.FieldName == metadata.PropertyName);
            return dataDictionary != null && dataDictionary.IsRequired;

        }

    }
}