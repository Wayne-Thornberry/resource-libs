using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Res;
using Proline.Resource.Component.Events;
using System;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CScriptPosition
{
    public class ScriptPosContext : ComponentContext
    {
        public ScriptPosContext()
        { 
          //  ExportManager.CreateExport("IsPointWithinActivationRange", new Func<Vector3,bool>(IsPointWithinActivationRange));
        }

        public override void OnLoad()
        {
            var data =  new ResourceFileLoader().Load("data/scriptpositions.json"); 
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data); 
            ScriptPositionManager.AddScriptPositionPairs(scriptPosition.scriptPositionPairs); 
            PosBlacklist.Create(); 
        } 

        public override async Task OnTick()
        {
            //return;
            if (ScriptPositionManager.HasScriptPositionPairs())
            {
                foreach (var positionsPair in ScriptPositionManager.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, CitizenFX.Core.Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        _log.Debug("In range");
                        EventManager.InvokeEventV2("StartScriptHandler", positionsPair.ScriptName, vector); 
                        PosBlacklist.Add(positionsPair);
                    }
                    else if (World.GetDistance(vector, CitizenFX.Core.Game.PlayerPed.Position) > 10f && PosBlacklist.Contains(positionsPair))
                    {
                        PosBlacklist.Remove(positionsPair);
                    };
                }
            }
        } 
    }
}