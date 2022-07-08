using Proline.ClassicOnline.MData.Entity;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using System.Net.Sockets;
using Console = Proline.Resource.Console; 
using Proline.DBAccess.Proxies;
using Proline.Resource.Eventing;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Events;
using Proline.ClassicOnline.MData.Server.Internal;

namespace Proline.ClassicOnline.MData.Scripts
{
    public partial class DownloadSaveScript : ModuleScript
    { 

        public DownloadSaveScript() : base(true)
        {

        }

        public override async Task OnExecute()
        {
            var dq = DownloadQueue.GetInstance();
            while (dq.Count > 0)
            {
                var player = dq.Dequeue();
                await API.PullSaveFromCloudAsync(player, player.Name);
                PlayerReadyEvent.InvokeEvent();
            }

        }

    }
} 