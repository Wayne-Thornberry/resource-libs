using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Events
{
    public partial class SaveFileNetworkEvent : ExtendedEvent
    {

        public SaveFileNetworkEvent() : base(SAVEFILEHANDLER, false)
        {

        }
    }
}
