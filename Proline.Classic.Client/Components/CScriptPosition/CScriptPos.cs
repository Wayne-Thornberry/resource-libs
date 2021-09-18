using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Classic.Data;
using Proline.Classic.Managers;
using Proline.Engine;
using Proline.Engine.Componentry;
using Proline.Engine.Data;

namespace Proline.Classic.Components
{
    public class CScriptPos : ClientComponent
    {
        protected override void OnInitialized()
        {
            if (EngineConfiguration.IsClient)
            {
                var data = Resources.LoadFile(API.GetCurrentResourceName(), "data/scriptpositions.json");
                //LogDebug(data);
                var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);

                ScriptPositionManager.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);

                PosBlacklist.Create();
            }
        }

        protected override void OnStart()
        {


        }
        //protected override void OnFixedUpdate()
        //{
        //    if (EngineConfiguration.IsClient)
        //    {
        //        //return;
        //        if (ScriptPositionManager.HasScriptPositionPairs())
        //        {
        //            foreach (var positionsPair in ScriptPositionManager.GetScriptPositionsPairs())
        //            {
        //                var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
        //                if (World.GetDistance(vector, Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
        //                {
        //                    StartNewScript(positionsPair.ScriptName, vector);
        //                    PosBlacklist.Add(positionsPair);
        //                }
        //                else if (World.GetDistance(vector, Game.PlayerPed.Position) > 10f && PosBlacklist.Contains(positionsPair))
        //                {
        //                    PosBlacklist.Remove(positionsPair);
        //                };
        //            }
        //        }
        //    }
        //}
    }
}