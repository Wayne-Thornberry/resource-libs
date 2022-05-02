using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Resource.ModuleCore;
using Proline.Resource.Logging;
using System;
using System.Threading.Tasks;
using Proline.ClassicOnline.MData;

namespace Proline.ClassicOnline.MBrain
{
    public class ScriptPosContext : ModuleContext
    {
        private static Log _log = new Log();
        public ScriptPosContext()
        {
            //  ExportManager.CreateExport("IsPointWithinActivationRange", new Func<Vector3,bool>(IsPointWithinActivationRange));
        }

        public override void OnLoad()
        {
            var instance = ScriptPositionManager.GetInstance();
            var data = ResourceFile.LoadFile("data/scriptpositions.json");
            Resource.Console.Console.WriteLine(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);
            instance.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);
            PosBlacklist.Create();
        }

    }
}