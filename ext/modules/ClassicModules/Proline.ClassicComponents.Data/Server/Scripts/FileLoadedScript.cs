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

namespace Proline.ClassicOnline.MData.Scripts
{
    public partial class FileLoadedScript : ModuleScript
    { 

        public FileLoadedScript()
        {
        }

        public override async Task OnExecute()
        {
            LoadFileNetworkEvent.SubscribeEvent();
            SaveFileNetworkEvent.SubscribeEvent();
        }

    }
} 