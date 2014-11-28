using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;
using Pfizer.Repository;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Core.Web;
using Wizardsgroup.Core.Web.ExceptionHandlers;
using Wizardsgroup.Core.Web.ModelMetadataProvider;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Service;
using Wizardsgroup.Service.Attributes.ViewModelDictionary;
using Wizardsgroup.Service.Factories;
using Wizardsgroup.Utilities.Helpers;
using System.Collections.Generic;

namespace Pfizer.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {        
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
                        
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            SetupControllerBuilderNamespaces();
            SetupMetadataProviders();
            SetupValidatorProviders();

            //var models = ReflectionHelper.Instance.GetTypesFromAssembly("Pfizer.Web")
            //    .Where(type => type.GetCustomAttributes(typeof (ValidatorAttribute), true).FirstOrDefault() != null)
            //    .ToList();
              
            //models.ForEach(type=>ModelBinders.Binders.Add(type, new StringModelBinder()));
            
  
            var engine = new ExtendedRazorViewEngine();
            //engine.AddViewLocationFormat(string.Format("{0}.Proposal/Views/{0}.cshtml", area));
            // Add a shared location too, as the lines above are controller specific
            engine.AddPartialViewLocationFormat("~/Areas/Common/Views/Shared/{0}.cshtml");

            ViewEngines.Engines.Add(engine);

        }

        void Application_Error(object sender, EventArgs e)
        {
            var exceptionHandler = new GlobalAsaxErrorHandler(ReflectionHelper.Instance, new WebUtilityWrapper(Context));
            exceptionHandler.HandleException(Server.GetLastError(), Logger.Log);
        }

        private static void SetupControllerBuilderNamespaces()
        {
            const string area = "Pfizer.Web.Areas";
            ControllerBuilder.Current.DefaultNamespaces.Add(string.Format("{0}.Common.Controllers", area));
            ControllerBuilder.Current.DefaultNamespaces.Add(string.Format("{0}.Security.Controllers", area));
        }

        private static void SetupMetadataProviders()
        {
            //This will replace old datadictionary jquery call for replacing displaynames in Views            
            Expression<Func<IEntityService<DataDictionary>>> lazyLoadedService = () => new DataDictionaryService(new UnitOfWorkWrapper());
            ModelMetadataProviders.Current = new ViewModelCachedDataAnnotationsModelMetadataProvider(new ViewModelMetadataAttributeOverrider(lazyLoadedService));
        }

        private static void SetupValidatorProviders()
        {
            ValidatorOptions.CascadeMode = CascadeMode.Continue;
            //I dont know how to use IoC container resorting to use custom factor with ReflectionHelper and UnitOfWork as injected dependency
            //UnitOfWork is passed as Func<UnitOfWork> to lazy load the instantiation of database connection
            //Use fluentvalidation as validator provider for MVC
            Func<IUnitOfWork> lazyUnitOfWork = () => new UnitOfWorkWrapper();
            FluentValidationModelValidatorProvider.Configure(config =>
            {
                config.ValidatorFactory = new ValidatorFactory(ReflectionHelper.Instance, lazyUnitOfWork);
            });                        
            //Hook to validator display name event. This will change display name error based on datadictionary settings
            //MetaData is generated first and cached so its ok to get value from ViewModelDataDictionaryMetaModelAttributeOverrider internal dictionary for displayname in viewmodels
            ValidatorOptions.DisplayNameResolver = ViewModelMetadataAttributeOverrider.DisplayNameResolver;
            //To remove dataannotations conflict with fluentvalidation required field.
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        public class ExtendedRazorViewEngine : RazorViewEngine
        {
            public void AddViewLocationFormat(string paths)
            {
                var existingPaths = new List<string>(ViewLocationFormats) {paths};

                ViewLocationFormats = existingPaths.ToArray();
            }

            public void AddPartialViewLocationFormat(string paths)
            {
                var existingPaths = new List<string>(PartialViewLocationFormats) {paths};

                PartialViewLocationFormats = existingPaths.ToArray();
            }
        }

        #region Traverse all controllers within Pfizer.Web assembly

        //Lets find all the controllers in the assembly using reflection, remove also the assembly name
        public static readonly List<String> Global_ControllerList =
            (from controller in
                 (ReflectionHelper.Instance.GetTypesFromAssembly("Pfizer.Web")
                     .Where(type => typeof(Controller).IsAssignableFrom(type)))
             select controller.FullName.Substring("Pfizer.Web".Length + 1)).ToList();
        #endregion

    }
}