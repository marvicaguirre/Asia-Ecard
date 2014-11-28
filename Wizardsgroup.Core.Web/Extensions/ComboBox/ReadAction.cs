using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ReadAction : IReadAction
    {
        public ReadActionConfigContainer Configuration { get; private set; }
        
        public ReadAction()
        {
            Configuration = new ReadActionConfigContainer();    
        }
        
        public IReadAction Action(string action, string controller)
        {
            action.Guard("Action must not be null.");
            controller.Guard("Controller must not be null.");
            Configuration.Action = action;
            Configuration.Controller = controller;
            return this;
        }

        public IReadAction Parameter(params string[] paramControls)
        {
            paramControls.Guard("Parameter must not be null.");
            Configuration.Parameter = paramControls;
            return this;
        }
    }
}