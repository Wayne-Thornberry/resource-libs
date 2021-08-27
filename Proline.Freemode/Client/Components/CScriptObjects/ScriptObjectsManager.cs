extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;

using Newtonsoft.Json;
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Components.CScriptObjects
{


    internal class SOP
    {
        public int Hash { get; set; }
        public List<ScriptObjectPair>  Pairs {get; set;}
    }

    internal static class ScriptObjectsManager
    {
        private static Dictionary<int, SOP> _scriptObjectPairs;

        internal static bool HasScriptObjectPairs()
        {
            return _scriptObjectPairs.Count > 0;
        }

        internal static void AddScriptObjectPairs(ScriptObjectPair[] scriptObjectPairs)
        {
            _scriptObjectPairs = new Dictionary<int, SOP>();
            foreach (var item in scriptObjectPairs)
            {
                var modelHash = item.ModelName;
                if (modelHash == 0)
                    modelHash =  API.GetHashKey(item.ModelHash);
                if (!_scriptObjectPairs.ContainsKey(modelHash))
                {
                    _scriptObjectPairs.Add(modelHash, new SOP() { Hash = modelHash, Pairs = new List<ScriptObjectPair>() });
                };
                _scriptObjectPairs[modelHash].Pairs.Add(item);
            }
        }

        internal static SOP GetScriptObjectPair(int modelHash)
        {
            if (_scriptObjectPairs.ContainsKey(modelHash))
                return _scriptObjectPairs[modelHash];
            else
                return null;
        }
    }
}
