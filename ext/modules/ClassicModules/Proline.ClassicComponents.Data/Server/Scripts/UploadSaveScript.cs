﻿using Proline.ClassicOnline.MData.Entity;
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
    public partial class UploadSaveScript : ModuleScript
    { 

        public UploadSaveScript() : base(true)
        {

        }

        public override async Task OnExecute()
        {
            var uq = UploadQueue.GetInstance();
            while(uq.Count > 0)
            {
                var save = uq.Dequeue(); 
                await API.SendSaveToCloudAsync(save.Owner, save); 
            }
        }

    }
} 