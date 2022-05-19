using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Resource.ModuleCore;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proline.ClassicOnline.MBrain.Data;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.Resource.Console;
using Proline.ClassicOnline.MDebug;

namespace Proline.ClassicOnline.MBrain
{
    public class MBrainContext : ModuleContext
    {
        private static Log _log = new Log();

        public MBrainContext()
        {
        }

        public override void OnLoad()
        {
            var instance = ScriptPositionManager.GetInstance();
            var data = MDataAPI.LoadFile("data/scriptpositions.json");
            MDebug.MDebugAPI.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);
            instance.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);
            PosBlacklist.Create();

            var data2 = MDataAPI.LoadFile("data/scriptobjects.json");
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data2);
            var sm = ScriptObjectManager.GetInstance();

            foreach (var item in objs)
            {
                var hash = string.IsNullOrEmpty(item.ModelHash) ? item.ModelName : API.GetHashKey(item.ModelHash);
                if (!sm.ContainsKey(hash))
                    sm.Add(hash, new List<ScriptObjectData>());
                sm.Get(hash).Add(item);
            }
            base.OnLoad();
        }






    }
}