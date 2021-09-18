using CitizenFX.Core;
using Proline.Classic.Managers;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.APIs
{
    public static class BrainAPI
    { 
        public static void StartNewEntityScript(string scriptName, int handle, params object[] param)
        {
            var args = new List<object>(param);
            args.Add(handle);
            args.AddRange(param);
            Script.StartScript(scriptName, args.ToArray());
        }

        public static bool IsEntityInActivationRange(int entHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entHandle);
            if (tracted == null) return false;
            return tracted.Scripts.Select(e => e.ActivationRange).First() < 10f;
        }
        public static bool IsInActivationRange(Vector3 vector3)
        {
            return World.GetDistance(Game.PlayerPed.Position, vector3) < 10f;
        }

        public static int[] GetScriptHandlesFromEntity(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            if (tracted == null) return new int[0];
            return tracted.Scripts.Select(e => e.ScriptHandle).ToArray();
        }

        public static bool IsEntityScripted(int entityHandle)
        {
            var _tom = TrackedObjectsManager.GetInstance();
            var tracted = _tom.Get(entityHandle);
            if (tracted == null) return false;
            return tracted.Scripts.Select(e => e.ActivationRange).First() < 10f;
        }
    }
}
