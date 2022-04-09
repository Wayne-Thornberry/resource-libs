using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.Networking
{
    public class EventSocket
    {
        private string _endPoint;
        private Delegate _action;
        public string EndPoint => string.Format("es:{0}",_endPoint);
        public Delegate Action => _action;


        public EventSocket(string endPoint)
        {
            _endPoint = endPoint;
        }

        public void SetCallback(Delegate action)
        {
            _action = action;
        }
    }
}
