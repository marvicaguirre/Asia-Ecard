using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Extensions
{
    public class ClientEventConfigContainer
    {
        public ClientEventConfigContainer()
        {
            ListOfClientBlurEvents = new List<string>();
            ListOfClientChangeEvents = new List<string>();
        }
        public List<string> ListOfClientChangeEvents { get; private set; }
        public List<string> ListOfClientBlurEvents { get; private set; }
    }
}