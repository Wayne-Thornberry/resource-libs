using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Modularization.Core;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proline.ClassicOnline.MBrain.Data;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.Resource;
using Proline.ClassicOnline.MDebug;
using System.Reflection;
using Proline.ClassicOnline.MScripting;
using Proline.ClassicOnline.MBrain.Events;

namespace Proline.ClassicOnline.MBrain
{
    public class MBrainContext : ModuleScript
    {
        private static Log _log = new Log();

        public MBrainContext(Assembly source) : base(source)
        {
            _trackedHandles = new HashSet<int>();
            _ht = HandleTracker.GetInstance();
        }


        private ScriptObjectManager _sm;
        private HashSet<int> _trackedHandles;
        private HandleTracker _ht; 

        private void ProcessScriptObjects()
        {
            var values = _sm.GetValues();
            if (values == null)
                return;
            var quew = new Queue<ScriptObject>(values);
            while (quew.Count > 0)
            {
                var so = quew.Dequeue();
                ProcessScriptObject(so);
            }
        }
        private void ProcessScriptObject(ScriptObject so)
        {
            if (!API.DoesEntityExist(so.Handle))
            {
                _sm.Remove(so.Handle);
                return;
            }
            var entity = CitizenFX.Core.Entity.FromHandle(so.Handle);
            foreach (var item in so.Data)
            {
                if (IsEntityWithinActivationRange(entity, Game.PlayerPed, item.ActivationRange) && so.State == 0)
                {
                    _log.Debug(so.Handle + " Player is within range here, we should start the script and no longer track this for processing");
                    MScriptingAPI.StartNewScript(item.ScriptName, so.Handle);
                    so.State = 1;
                    _sm.Remove(so.Handle);
                    return;
                }
            }
        }


        private bool IsEntityWithinActivationRange(CitizenFX.Core.Entity entity, CitizenFX.Core.Entity playerPed, float activationRange)
        {
            var pos = Game.PlayerPed.Position;
            var pos2 = entity.Position;
            return API.Vdist2(pos.X, pos.Y, pos.Z, pos2.X, pos2.Y, pos2.Z) <= activationRange;
        }

        public override async Task OnStart()
        {
            _sm = ScriptObjectManager.GetInstance();
            var onNewHandlesFound = new OnNewHandlesFound();
            var onEntityUntracked = new OnEntityUntracked();

            _ht = HandleTracker.GetInstance();
            _ht.EntityHandleTracked += onNewHandlesFound.OnEventInvoked;
            _ht.EntityHandleUntracked += onEntityUntracked.OnEventInvoked;
        }

        public override async Task OnLoad()
        {
            var instance = ScriptPositionManager.GetInstance();
            var data = MDataAPI.LoadResourceFile("data/scriptpositions.json");
            MDebug.MDebugAPI.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data);
            instance.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);
            PosBlacklist.Create();

            var data2 = MDataAPI.LoadResourceFile("data/scriptobjects.json");
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data2);
            var sm = ScriptObjectManager.GetInstance();

            foreach (var item in objs)
            {
                var hash = string.IsNullOrEmpty(item.ModelHash) ? item.ModelName : API.GetHashKey(item.ModelHash);
                if (!sm.ContainsKey(hash))
                    sm.Add(hash, new List<ScriptObjectData>());
                sm.Get(hash).Add(item);
            }

            var entityHandles = new Queue<int>(HandleManager.EntityHandles);
            var addedHandles = new List<object>();
            var removedHanldes = new List<object>();

            while (entityHandles.Count > 0)
            {
                var handle = entityHandles.Dequeue();
                if (_trackedHandles.Contains(handle))
                    continue;
                _trackedHandles.Add(handle);
                addedHandles.Add(handle);
                _ht.Add(handle);
            }

            var combinedHandles = new Queue<int>(_trackedHandles);
            while (combinedHandles.Count > 0)
            {
                var handle = combinedHandles.Dequeue();
                if (API.DoesEntityExist(handle))
                    continue;
                _trackedHandles.Remove(handle);
                removedHanldes.Add(handle);
                _ht.Remove(handle);
            }
            await Delay(1000);

            ProcessScriptObjects();
            await Delay(10);
            //return;
            instance = ScriptPositionManager.GetInstance();

            if (instance.HasScriptPositionPairs())
            {
                foreach (var positionsPair in instance.GetScriptPositionsPairs())
                {
                    var vector = new Vector3(positionsPair.X, positionsPair.Y, positionsPair.Z);
                    if (World.GetDistance(vector, Game.PlayerPed.Position) < 10f && !PosBlacklist.Contains(positionsPair))
                    {
                        Resource.Console.WriteLine(_log.Debug("In range"));
                        MScriptingAPI.StartNewScript(positionsPair.ScriptName, vector);
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