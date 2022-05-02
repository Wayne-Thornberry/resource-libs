using CitizenFX.Core;
using Proline.ClassicOnline.MBrain;
using Proline.ClassicOnline.MScripting;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Scripts
{
    internal class ProcessScriptAreas : ModuleScript
    {
        public ProcessScriptAreas()
        {
        }

        public override async Task OnUpdate()
        {
            //return;
            var instance = ScriptPositionManager.GetInstance();

            if (instance.HasScriptPositionPairs())
            {
                foreach (var positionsPair in instance.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        Resource.Console.Console.WriteLine(_log.Debug("In range"));
                        Script.StartNewScript(positionsPair.ScriptName, vector);
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
