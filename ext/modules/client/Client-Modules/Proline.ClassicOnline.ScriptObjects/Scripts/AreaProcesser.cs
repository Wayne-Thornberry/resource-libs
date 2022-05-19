using CitizenFX.Core;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.ClassicOnline.MScripting;
using Proline.Resource.Console;
using Proline.Resource.ModuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Scripts
{
    internal class AreaProcesser : ModuleScript
    {
        public AreaProcesser()
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
                        EConsole.WriteLine(_log.Debug("In range"));
                        MScriptingAPI.StartNewScript(positionsPair.ScriptName, vector);
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
