using System.Linq;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ClientEvent : IClientEvent
    {
        public ClientEventConfigContainer ClientEventContainer { get; private set; }

        public ClientEvent()
        {
            ClientEventContainer = new ClientEventConfigContainer();
        }

        public IClientEvent Change(string change)
        {
            if (!ClientEventContainer.ListOfClientChangeEvents.Any(o => o.ToLower().Equals(change.ToLower())))
                ClientEventContainer.ListOfClientChangeEvents.Add(change);

            return this;
        }

        public IClientEvent Blur(string blur)
        {
            if (!ClientEventContainer.ListOfClientBlurEvents.Any(o => o.ToLower().Equals(blur.ToLower())))
                ClientEventContainer.ListOfClientBlurEvents.Add(blur);

            return this;
        }
    }
}