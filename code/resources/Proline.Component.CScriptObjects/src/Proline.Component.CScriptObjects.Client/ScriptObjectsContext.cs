using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Component.Framework.Client.Access;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CScriptObjects
{
    public class ScriptObjectsContext : ComponentContext
    {
        private List<ScriptObjectData> _scriptObjData;
        private List<ScriptObject> _scriptObjs;

        private CEvent _entityTrackedHandler;
        private CEvent _entityUnTrackedHandler;

        public ScriptObjectsContext()
        { 
            _scriptObjData = new List<ScriptObjectData>();
            _scriptObjs = new List<ScriptObject>();
            _entityTrackedHandler = new CEvent("CObjectTracker", "EntityHandleTracked");
            _entityUnTrackedHandler = new CEvent("CObjectTracker", "EntityHandleUnTracked");

            EventHandlers.Add("EntityHandleTracked", new Action<int>(OnEntityTracked));
            EventHandlers.Add("EntityHandleUnTracked", new Action<int>(OnEntityUntracked));
        }

        public override void OnLoad()
        {

            var rfl = new ResourceFileLoader();
            var data = rfl.Load("data/scriptobjects.json");
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data);


            //_scriptObjData.Add(new ScriptObjectData()
            //{
            //    ActivationRange = 30f,
            //    ModelHash = "prop_atm_03",
            //    ModelName = -1,
            //    ScriptName = "ObAtm"
            //});



            _scriptObjData.AddRange(objs);

            _entityTrackedHandler.AddListener(new Action<int>(OnEntityTracked));
            _entityUnTrackedHandler.AddListener(new Action<int>(OnEntityUntracked));

            base.OnLoad();
        }

        private void OnEntityUntracked(int handle)
        {
            //_log.Debug(handle + " Old handle untracked, if a tracked obj existed here but had not been activated, we remove it");
            var entity = Entity.FromHandle(handle);
            var so = _scriptObjs.Where(e => e.Handle == handle).FirstOrDefault(); 
            if(so != null)
                _scriptObjs.Remove(so);
            ProcessScriptObjects();
        }

        private void OnEntityTracked(int handle)
        {
           // _log.Debug(handle + " New handle found, time to see if we can start a script from it");
            var entity = Entity.FromHandle(handle);
            if (!entity.Exists()) return;

            var data = _scriptObjData.Where(e => API.GetHashKey(e.ModelHash) == entity.Model).FirstOrDefault();
            if (data == null)
                return;

            _log.Debug(handle + " Oh boy, we found a matching script object with that model hash from that handle, time to track it");

            var so = new ScriptObject()
            {
                Data = data,
                Handle = handle,
                State = 0,
            };

            if (!_scriptObjs.Contains(so))
                _scriptObjs.Add(so);

            ProcessScriptObjects();
        }

        private void ProcessScriptObjects()
        {
            foreach (var so in _scriptObjs.ToArray())
            {
                ProcessScriptObject(so);
            }
        }

        public void StartNewScript(string name, params object[] args)
        {
            BaseScript.TriggerEvent("StartScriptHandler", name, new List<object>(args)); 
        }


        private void ProcessScriptObject(ScriptObject so)
        {
            var entity = Entity.FromHandle(so.Handle);
            if (entity == null)
            {
                _scriptObjs.Remove(so);
                return;
            }


            // _log.Debug(so.Handle + " Processing script object to see if the player is in activation range");
            if (IsEntityWithinActivationRange(entity, Game.PlayerPed, so.Data.ActivationRange))
            { 
                _log.Debug(so.Handle + " Player is within range here, we should start the script and no longer track this for processing");
                //var si = ScriptManager.GetInstance();
                StartNewScript(so.Data.ScriptName, so.Handle);
                so.State = -1;
                _scriptObjs.Remove(so);
            }
            else
            {
               // _log.Debug(so.Handle + " Player is not in range yet, we should see if this obj is tracked, if not, we should add it for processing later");
                so.State = 1;
                if (!_scriptObjs.Contains(so))
                    _scriptObjs.Add(so);
            }
        }

        private bool IsEntityWithinActivationRange(Entity entity, Entity playerPed, float activationRange)
        {
            return World.GetDistance(entity.Position, playerPed.Position) <= activationRange;
        }
         

        public override void OnStart()
        {

        }
    }
}