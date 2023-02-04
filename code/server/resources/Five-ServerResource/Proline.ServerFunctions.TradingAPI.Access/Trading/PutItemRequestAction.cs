using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerAccess.IO.Client.Trading
{
    internal class PutItemRequestAction : ExtendedEvent
    { 

        public PutItemRequestAction() : base("PutItemRequestHandler", false)
        {

        } 
    }
}
