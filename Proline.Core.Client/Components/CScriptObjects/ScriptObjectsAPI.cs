using CitizenFX.Core;
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CScriptObjects
{
    public class ScriptObjectsAPI : ComponentAPI
    {
        [Client]
        [ComponentAPI]
        public void StartNewEntityScript(string scriptName, int handle, params object[] param)
        {
            var args = new List<object>(param);
            args.Add(handle);
            args.AddRange(param);
            EngineAccess.StartNewScript(scriptName, args.ToArray());
        }

        [Client]
        [ComponentAPI]
        public bool IsEntityInActivationRange(int entityHandle)
        {
            var handle = Entity.FromHandle(entityHandle);
            return World.GetDistance(handle.Position, Game.PlayerPed.Position) < 10f;
        }

        [Client]
        [ComponentAPI]
        public void Test(string x, string y, string z)
        {

            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
        }

        [Client]
        [ComponentAPI]
        public void Wow2()
        {

        }

        [Client]
        [ComponentAPI]
        public void testc()
        {

        }
    }
}
