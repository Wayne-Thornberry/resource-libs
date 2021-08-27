extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using Newtonsoft.Json;

using System;
using System.Threading.Tasks;
using System.Reflection;
using Proline.Engine;

using System.Security; 
using Proline.Engine;

namespace Proline.Freemode.Components.CScriptPos
{
    public class ScriptPosComponent : EngineComponent
    {
        [Client]
        [ComponentAPI]
        [SuppressUnmanagedCodeSecurity]
        public bool IsInActivationRange(Vector3 vector3)
        {
            return World.GetDistance(Game.PlayerPed.Position, vector3) < 10f;
        }


        protected override void OnInitialized()
        {
            if (EngineService.IsClient)
            {
                var data = ResourceFile.Load(API.GetCurrentResourceName(), "data/scriptpositions.json");
                //LogDebug(data);
                var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);

                ScriptPositionManager.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);

                PosBlacklist.Create();
            }
        }

        protected override void OnStart()
        {


        }
        protected override void OnFixedUpdate()
        {
            if (EngineService.IsClient)
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
}