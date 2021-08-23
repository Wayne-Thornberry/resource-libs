using CitizenFX.Core;
using Proline.Engine;
using Proline.Engine;
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
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            if (tracted == null) return false;
            return tracted.Scripts.Select(e=>e.ActivationRange).First() < 10f;
        }

        [Client]
        [ComponentAPI]
        public bool IsEntityScripted(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            return tracted != null;
        }

        [Client]
        [ComponentAPI]
        public int[] GetScriptHandlesFromEntity(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            if (tracted == null) return new int[0];
            return tracted.Scripts.Select(e=>e.ScriptHandle).ToArray();
        }
    }
}
