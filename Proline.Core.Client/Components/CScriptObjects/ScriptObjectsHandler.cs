using CitizenFX.Core.Native;
using Newtonsoft.Json;

using System;
using System.Threading.Tasks;
using System.Reflection;
using Proline.Framework;
using CitizenFX.Core;
using System.Collections.Generic; 
using Proline.Engine;

namespace Proline.Core.Client.Components.CScriptObjects
{
    public class ScriptObjectsHandler : ComponentHandler
    {
        public override void OnComponentInitialized()
        {

            var data = ResourceFile.Load(API.GetCurrentResourceName(), "data/scriptobjects.json");
            Debugger.LogDebug(data);
            var scriptObjects = JsonConvert.DeserializeObject<ScriptObj>(data);

            ScriptObjectsManager.AddScriptObjectPairs(scriptObjects.scriptObjectPairs);

            ObjectBlacklist.Create();

        }


        public override void OnComponentStart()
        {

        }
    }
}