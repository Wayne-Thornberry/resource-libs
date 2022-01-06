using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Component.Framework.Client.Global;
using Proline.Resource.Client.Eventing;
using Proline.Resource.Client.Framework;
using Proline.Resource.Client.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Proline.Classic.Engine.Components.CDebugInterface
{
    public class DebugInterfaceContext : ComponentContext
    {
        private CEvent _entityHandleTrackedHandler;
        private CEvent _entityHandleUnTrackedHandler;
        private List<int> _handles;

        public DebugInterfaceContext()
        {
            //_entityHandleTrackedHandler = new ComponentEvent("CObjectTracker", "EntityHandleTracked");
            //_entityHandleUnTrackedHandler = new ComponentEvent("CObjectTracker", "EntityHandleUnTracked");
            _handles = new List<int>();
            EventHandlers.Add("EntityHandleTracked", new Action<int>(OnEntityTracked));
            EventHandlers.Add("EntityHandleUnTracked", new Action<int>(OnEntityUntracked));
        }

        public override void OnLoad()
        {
            // _handles = (List<int>)MemoryCache.Retrive("TrackedHandles");
            //_entityHandleTrackedHandler = EventManager.RegisterEvent("EntityHandleTracked");
            //_entityHandleUnTrackedHandler = EventManager.RegisterEvent("EntityHandleUnTracked");


            //_entityHandleTrackedHandler.AddListener(new Action<int>(OnEntityTracked));
            //_entityHandleUnTrackedHandler.AddListener(new Action<int>(OnEntityUntracked));

            var x = Globals.GetGlobal("Testing");
            _log.Debug("Global found testing " + x);
        }
        public override void OnStart()
        {

        }

        private void OnEntityUntracked(int handle)
        {
            _handles.Remove(handle);
        }

        private void OnEntityTracked(int handle)
        {
            _handles.Add(handle);
        }

        public override async Task OnTick()
        {
            var t = CitizenFX.Core.Game.PlayerPed.Position.ToString() + "H:" + CitizenFX.Core.Game.PlayerPed.Heading + "\n"
               + CitizenFX.Core.Game.PlayerPed.Model.Hash + "\n"
               + CitizenFX.Core.Game.PlayerPed.Health + "\n"
               + CitizenFX.Core.Game.PlayerPed.Handle + "\n" +
               _handles.Count + " Entities in the world ";
            DebugUtil.DrawDebugText2D(t, new PointF(0.1f, 0.05f), 0.3f, 0);
            foreach (var handle in _handles)
            {
                var entity = Entity.FromHandle(handle);
                //_log.Debug(API.GetEntityType(handle).ToString());
                if (entity == null) continue;
               if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, CitizenFX.Core.Game.PlayerPed.Position) > 10f) continue;
               // var pos = entity.Model.GetDimensions();
                var d = entity.Position + new Vector3(0, 0, (entity.Model.GetDimensions().Z * 0.8f));
                var x = $"{entity.Handle}\n" +
                    $"{entity.Model.Hash}\n" +
                    $"{Math.Round(entity.Heading, 2)}\n" +
                    $"{entity.Health}\n";// +
                                         //$"{ExampleAPI.IsEntityScripted(entity.Handle)}";
                //DebugUtil.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
                DebugUtil.DrawDebugText3D(x, d, 3f, 0);
            }
        }
    }
}
