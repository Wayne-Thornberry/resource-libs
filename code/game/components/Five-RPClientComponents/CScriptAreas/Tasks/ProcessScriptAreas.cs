using CCoreSystem;
using CitizenFX.Core;
using CScriptAreas.Entity;

using Proline.Resource.Logging;
using System.Threading.Tasks;

namespace CScriptAreas.Tasks
{
    public class ProcessScriptAreas
    {
        private static Log _log = new Log();

        public ProcessScriptAreas()
        {
        }


        public async Task Execute()
        {
            var instance = ScriptPositionManager.GetInstance();
            //return; 

            if (instance.HasScriptPositionPairs())
            {
                foreach (var positionsPair in instance.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        Proline.Resource.Console.WriteLine(_log.Debug("In range"));
                        var api = new CCoreSystemAPI();
                        api.StartNewScript(positionsPair.ScriptName, vector);
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