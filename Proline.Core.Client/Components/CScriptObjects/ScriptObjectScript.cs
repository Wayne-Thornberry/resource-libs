using System;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

using Proline.Framework; 

using Proline.Core.Client;
using CitizenFX.Core.Native;
using Proline.Core.Client;
using Proline.Engine;

namespace Proline.Core.Client.Components.CScriptObjects
{
    public class ScriptObjectScript : ComponentScript
    {
        private List<int> _handles;

        public override void Start()
        {
            _handles = new List<int>();
        }
        public override void Update()
        {

        }

        public override void OnEngineEvent(string eventName, params object[] args)
        {
            if (((bool)args[0]))
            {
                _handles.Add((int)args[1]);
                //Debugger.LogDebug((int)args[0]);
            }
            else 
            {
                _handles.Remove((int)args[1]);
                //Debugger.LogDebug((int)args[0]);
            }
        }

        public override void FixedUpdate()
        {
           // return;
            if (ScriptObjectsManager.HasScriptObjectPairs())
            { 
                foreach (var item in ObjectBlacklist.GetList())
                { 
                    if (!API.DoesEntityExist(item))
                        ObjectBlacklist.Remove(item);
                }

                foreach (var handle in _handles)
                {
                    var blacklist = false;
                    foreach (var item in ScriptObjectsManager.GetScriptObjectPairs())
                    {
                        // TODO, Wrap this on the engine
                        var modelHash = API.GetEntityModel(handle);
                        if(item.ModelName != 0)
                        {
                            blacklist = NewMethod(handle, blacklist, item, modelHash, item.ModelName); 
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(item.ModelHash))
                            {
                                var hash = API.GetHashKey(item.ModelHash);
                                blacklist = NewMethod(handle, blacklist, item, modelHash, hash);
                            }
                        }
                    }
                    if (blacklist && !ObjectBlacklist.Contains(handle))
                        ObjectBlacklist.Add(handle);
                }
            }
        }

        private static bool NewMethod(int handle, bool blacklist, ScriptObjectPair item, int modelHash, int hash)
        {
            if (hash == modelHash && !ObjectBlacklist.Contains(handle))
            {
                Debugger.LogDebug("Found new object that should start a script");
                EngineAccess.StartNewScript(item.ScriptName, new object[1] { handle });
                blacklist = true;
            }
            return blacklist;
        }
    }
}
