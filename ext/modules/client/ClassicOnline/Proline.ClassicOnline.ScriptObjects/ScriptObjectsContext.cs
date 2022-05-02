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

namespace Proline.ClassicOnline.MBrain
{
    public class ScriptObjectsContext : ModuleContext
    {
        private static Log _log = new Log();

        public ScriptObjectsContext()
        {
        }

        public override void OnLoad()
        {

            var data = ResourceFile.LoadFile("data/scriptobjects.json");
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data);
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