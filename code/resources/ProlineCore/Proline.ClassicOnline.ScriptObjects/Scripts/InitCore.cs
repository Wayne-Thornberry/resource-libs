using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MBrain.Data;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MScripting.Events;
using Proline.ClassicOnline.MScripting.Internal;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.S
{
    public class InitCore
    { 

        public async Task Execute()
        {
            var instance = ScriptPositionManager.GetInstance();

            var data = ResourceFile.Load("data/brain/scriptpositions.json");
            MDebugAPI.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data.Load());
            instance.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);
            PosBlacklist.Create();

            var data2 = ResourceFile.Load("data/brain/scriptobjects.json");
            MDebugAPI.LogDebug(data2);
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data2.Load());
            var sm = ScriptObjectManager.GetInstance();

            foreach (var item in objs)
            {
                var hash = string.IsNullOrEmpty(item.ModelHash) ? item.ModelName : CitizenFX.Core.Native.API.GetHashKey(item.ModelHash);
                if (!sm.ContainsKey(hash))
                    sm.Add(hash, new List<ScriptObjectData>());
                sm.Get(hash).Add(item);
            }
        }
    }
}
