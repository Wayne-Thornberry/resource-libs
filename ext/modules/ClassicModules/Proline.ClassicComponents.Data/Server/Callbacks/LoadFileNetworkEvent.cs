using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.DBAccess.Proxies;
using System.Net.Sockets;
using CitizenFX.Core;
using Console = Proline.Resource.Console;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Server.Internal;

namespace Proline.ClassicOnline.MData
{
    public partial class LoadFileNetworkEvent : ExtendedEvent
    {
        public LoadFileNetworkEvent() : base(LOADFILEHANDLER)
        { 
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            // old way via getting id
            if(args.Length > 0)
            {  
                var sm = DataFileManager.GetInstance();
                if (!sm.DoesPlayerHaveSave(player))
                    return null;
                return sm.GetSave(player).GetSaveFiles(); 
            }
            else
            {
                Console.WriteLine("Argument count does not match expected count");
                return null;
            }
        }

        public static LoadFileNetworkEvent TriggerEvent(Player player, long id)
        {
            var x = new LoadFileNetworkEvent();
            x.Invoke(player, id);
            return x;
        }

    }
}
