using System;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CitizenFX.Core.Native;
using System.Collections.Generic;
using CitizenFX.Core;

using System.Drawing;
using Proline.Framework;
using Proline.Core.Client;
using Proline.Engine;

namespace Proline.Core.Client.Components.CScriptPos
{
    public class ScriptPosScript : ComponentScript
    { 
        public override void Update()
        { 

        }

        public override void FixedUpdate()
        {
            //return;
            if (ScriptPositionManager.HasScriptPositionPairs())
            {
                foreach (var positionsPair in ScriptPositionManager.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        EngineAccess.StartNewScript(positionsPair.ScriptName, vector);
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
