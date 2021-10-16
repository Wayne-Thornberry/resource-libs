using CitizenFX.Core;
using Proline.Game.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CScriptPosition
{
    internal class ScriptPosScript : ComponentScript
    {
        protected override void OnStart()
        {


        }
        protected override void OnFixedUpdate()
        {
            //return;
            if (ScriptPositionManager.HasScriptPositionPairs())
            {
                foreach (var positionsPair in ScriptPositionManager.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, CitizenFX.Core.Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        StartNewScript(positionsPair.ScriptName, vector);
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
