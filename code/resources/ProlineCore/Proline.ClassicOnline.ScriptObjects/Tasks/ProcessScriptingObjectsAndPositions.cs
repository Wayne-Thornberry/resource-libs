using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;

using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proline.ClassicOnline.MBrain.Data;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MBrain.Entity;
using Proline.CFXExtended.Core;
using Proline.Resource;
using Proline.ClassicOnline.MDebug;
using System.Reflection;
using Proline.ClassicOnline.MScripting;
using Proline.Resource.IO;

namespace Proline.ClassicOnline.MBrain.Tasks
{
    public class ProcessScriptingObjectsAndPositions
    {
        private static Log _log = new Log();

        public ProcessScriptingObjectsAndPositions()
        {
            _trackedHandles = new HashSet<int>();
        }


        private HashSet<int> _trackedHandles;
        private ScriptObjectManager _sm;

        public async Task Execute()
        {

            _sm = ScriptObjectManager.GetInstance();
            var _instance = ScriptPositionManager.GetInstance();
            var _ht = HandleTracker.GetInstance();

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

                if (!CitizenFX.Core.Native.API.DoesEntityExist(handle)) continue;
                var modelHash = CitizenFX.Core.Native.API.GetEntityModel(handle);
                if (!_sm.ContainsSO(handle) && _sm.ContainsKey(modelHash))
                {
                    _log.Debug(handle + " Oh boy, we found a matching script object with that model hash from that handle, time to track it");
                    _sm.AddSO(handle, new ScriptObject()
                    {
                        Data = _sm.Get(modelHash),
                        Handle = handle,
                        State = 0,
                    });
                }
            }

            var combinedHandles = new Queue<int>(_trackedHandles);
            while (combinedHandles.Count > 0)
            {
                var handle = combinedHandles.Dequeue();
                if (CitizenFX.Core.Native.API.DoesEntityExist(handle))
                    continue;
                _trackedHandles.Remove(handle);
                removedHanldes.Add(handle);
                _ht.Remove(handle);

                if (CitizenFX.Core.Native.API.DoesEntityExist(handle)) continue;
                var modelHash = CitizenFX.Core.Native.API.GetEntityModel(handle);
                if (!_sm.ContainsKey(modelHash)) continue;
                if (_sm.ContainsKey(handle))
                    _sm.Remove(handle);
            }

            ProcessScriptObjects();
            //return; 

            if (_instance.HasScriptPositionPairs())
            {
                foreach (var positionsPair in _instance.GetScriptPositionsPairs())
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
            if (!CitizenFX.Core.Native.API.DoesEntityExist(so.Handle))
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
            return CitizenFX.Core.Native.API.Vdist2(pos.X, pos.Y, pos.Z, pos2.X, pos2.Y, pos2.Z) <= activationRange;
        }



    }
}