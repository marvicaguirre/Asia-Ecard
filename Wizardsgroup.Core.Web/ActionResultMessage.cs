using System.Collections.Generic;

namespace Wizardsgroup.Core.Web
{
    public class ActionResultMessage
    {
        public ActionResultMessage()
        {
            Messages = new List<string>();
        }
        public string ActionStatus;
        public IList<string> Messages;
    }
}