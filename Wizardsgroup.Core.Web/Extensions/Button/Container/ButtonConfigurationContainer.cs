using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public class ButtonConfigurationContainer
    {
        public ButtonConfigurationContainer()
        {
            Button = new ButtonPropertyContainer();
            Modal = new ModalProperyContainer();
            Controller = new ControllerPropertyContainer();
            SelectionMode = SelectionMode.Multiple;        
            ClientAction = new ClientAction();            
        }
        public HtmlHelper HtmlHelper { get; set; }
        public ButtonPropertyContainer Button { get; set; }
        public ModalProperyContainer Modal { get; set; }
        public ControllerPropertyContainer Controller { get; set; }
        public SelectionMode SelectionMode { get; set; }
        public ClientAction ClientAction { get; set; }
        public object Route { get; set; }
    }
}