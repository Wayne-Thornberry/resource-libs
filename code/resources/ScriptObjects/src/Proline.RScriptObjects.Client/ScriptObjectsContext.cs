using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Resource.Framework;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CScriptObjects
{
    public class ScriptObjectsContext : ComponentContext
    {
        private Dictionary<int, List<ScriptObjectData>> _scriptObjData;
        private Dictionary<int, ScriptObject> _processScriptObjs;

        public ScriptObjectsContext()
        { 
            _scriptObjData = new Dictionary<int, List<ScriptObjectData>>();
            _processScriptObjs = new Dictionary<int, ScriptObject>();

            EventManager.AddEventListenerV2("EntityHandlesTracked", new Action<List<object>>(OnNewHandlesFound)); 
            //EventManager.AddEventListenerV2("EntityHandlesUnTracked", new Action<List<object>>(OnEntityUntracked));
        }

        public override void OnLoad()
        {

            var rfl = new ResourceFileLoader();
            var data = rfl.Load("data/scriptobjects.json");
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data);

            foreach (var item in objs)
            {
                var hash = string.IsNullOrEmpty(item.ModelHash) ? item.ModelName : API.GetHashKey(item.ModelHash);
                if (!_scriptObjData.ContainsKey(hash))
                    _scriptObjData.Add(hash, new List<ScriptObjectData>());
                _scriptObjData[hash].Add(item);
            }
            base.OnLoad();
        }

        //private void OnEntityUntracked(List<object> handles)
        //{
        //    try
        //    {
        //        foreach (int handle in handles)
        //        {
        //            if (API.DoesEntityExist(handle)) return;
        //            var modelHash = API.GetEntityModel(handle);
        //            if (!_scriptObjData.ContainsKey(modelHash)) return;
        //            if (_scriptObjs.ContainsKey(handle))
        //                _scriptObjs.Remove(handle);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _log.Error(e.ToString(), true);
        //        throw;
        //    }
        //}

        private void OnNewHandlesFound(List<object> handles)
        {
            try
            {

                foreach (int handle in handles)
                {
                    if (!API.DoesEntityExist(handle)) return;
                    var modelHash = API.GetEntityModel(handle);
                    if (!_processScriptObjs.ContainsKey(handle) && _scriptObjData.ContainsKey(modelHash))
                    {
                        _log.Debug(handle + " Oh boy, we found a matching script object with that model hash from that handle, time to track it");
                        _processScriptObjs.Add(handle, new ScriptObject()
                        {
                            Data = _scriptObjData[modelHash],
                            Handle = handle,
                            State = 0,
                        });
                    }
                }
            }
            catch (Exception e)
            {

                _log.Error(e.ToString(), true);
                throw;
            }
        }

        public override async Task OnTick()
        {
            ProcessScriptObjects();
            await BaseScript.Delay(10);
        }

        private void ProcessScriptObjects()
        {
            var quew = new Queue<ScriptObject>(_processScriptObjs.Values);
            while (quew.Count > 0)
            { 
                var so = quew.Dequeue();
                ProcessScriptObject(so);
            }
        }

        public void StartNewScript(string name, int handle)
        {
            EventManager.InvokeEventV2("StartScriptHandler", name, handle);
        }


        private void ProcessScriptObject(ScriptObject so)
        {
            if (!API.DoesEntityExist(so.Handle))
            {
                _processScriptObjs.Remove(so.Handle);
                return;
            }
            var entity = Entity.FromHandle(so.Handle);
            foreach (var item in so.Data)
            {
                if (IsEntityWithinActivationRange(entity, Game.PlayerPed, item.ActivationRange) && so.State == 0)
                {
                    _log.Debug(so.Handle + " Player is within range here, we should start the script and no longer track this for processing");
                    StartNewScript(item.ScriptName, so.Handle);
                    so.State = 1;
                    _processScriptObjs.Remove(so.Handle);
                    return;
                }
            }
        }

        private bool IsEntityWithinActivationRange(Entity entity, Entity playerPed, float activationRange)
        {
            var pos = Game.PlayerPed.Position;
            var pos2 = entity.Position;
            return API.Vdist2(pos.X, pos.Y, pos.Z, pos2.X, pos2.Y, pos2.Z) <= activationRange;
        }
    }
}