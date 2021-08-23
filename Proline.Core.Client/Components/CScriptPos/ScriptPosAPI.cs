using CitizenFX.Core;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CScriptPos
{
    public class ScriptPosAPI : ComponentAPI
    {

        [Client]
        [ComponentAPI]
        [SuppressUnmanagedCodeSecurity]
        public bool IsInActivationRange(Vector3 vector3)
        {
            return World.GetDistance(Game.PlayerPed.Position, vector3) < 10f;
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
