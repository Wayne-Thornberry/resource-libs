using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerAccess.IO.Client.Trading
{
    internal class ListItemsRequestAction : ExtendedEvent
    {
        public string Data { get; set; }

        public ListItemsRequestAction() : base("ListItemsRequestHandler", true)
        {

        }
        protected override void OnEventCallback(params object[] args)
        {
            if (args != null)
            {
                // old way via getting id
                if (args.Length > 0)
                {
                    if (args[0] != null)
                        Data = args[0].ToString();
                }
            }
        }
    }
}
