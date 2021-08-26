
using Newtonsoft.Json;

using System;
using System.Threading.Tasks;
using System.Reflection;
using Proline.Engine;

using System.Security; 
using Proline.Engine;
using CitizenFX.Core.Native;
using CitizenFX.Core;

namespace Proline.Freemode.Components.CScriptPos
{
    public class ScriptPosHandler : ComponentHandler
    {
        public override void OnInitialized()
        {
            var data = ResourceFile.Load(API.GetCurrentResourceName(), "data/scriptpositions.json");
            //Debugger.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);

            ScriptPositionManager.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);

            PosBlacklist.Create();
        }

        public override void OnStart()
        {


        }
        public override void OnFixedUpdate()
        {
            //return;
            if (ScriptPositionManager.HasScriptPositionPairs())
            {
                foreach (var positionsPair in ScriptPositionManager.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        StartNewScript(positionsPair.ScriptName, vector);
                        PosBlacklist.Add(positionsPair);
                    }
                    else if (World.GetDistance(vector, Game.PlayerPed.Position) > 10f && PosBlacklist.Contains(positionsPair))
                    {
                        PosBlacklist.Remove(positionsPair);
                    };
                }
            }
        }
    }
}