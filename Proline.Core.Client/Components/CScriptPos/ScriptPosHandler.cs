using CitizenFX.Core.Native;
using Newtonsoft.Json;

using System;
using System.Threading.Tasks;
using System.Reflection;
using Proline.Framework;
using CitizenFX.Core;
using System.Security; 
using Proline.Engine;

namespace Proline.Core.Client.Components.CScriptPos
{
    public class ScriptPosHandler : ComponentHandler
    {
        public override void OnComponentInitialized()
        { 
            var data = ResourceFile.Load(API.GetCurrentResourceName(), "data/scriptpositions.json");
            Debugger.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);

            ScriptPositionManager.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);

            PosBlacklist.Create();
        }

        public override void OnComponentStart()
        {


        }

    }
}