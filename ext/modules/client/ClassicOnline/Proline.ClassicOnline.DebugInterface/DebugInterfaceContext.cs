using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Resource.ModuleCore; 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MRendering
{
    public class DebugInterfaceContext : ModuleContext
    {
        //private CEvent _entityHandleTrackedHandler;
        //private CEvent _entityHandleUnTrackedHandler;
        private List<int> _handles;

        public DebugInterfaceContext()
        {
            //_entityHandleTrackedHandler = new ComponentEvent("CObjectTracker", "EntityHandleTracked");
            //_entityHandleUnTrackedHandler = new ComponentEvent("CObjectTracker", "EntityHandleUnTracked");
            _handles = new List<int>();
            //EventHandlers.Add("EntityHandleTracked", new Action<int>(OnEntityTracked));
            //EventHandlers.Add("EntityHandleUnTracked", new Action<int>(OnEntityUntracked));
        }

        public override void OnLoad()
        {
            // _handles = (List<int>)MemoryCache.Retrive("TrackedHandles");
            //_entityHandleTrackedHandler = EventManager.RegisterEvent("EntityHandleTracked");
            //_entityHandleUnTrackedHandler = EventManager.RegisterEvent("EntityHandleUnTracked");


            //_entityHandleTrackedHandler.AddListener(new Action<int>(OnEntityTracked));
            //_entityHandleUnTrackedHandler.AddListener(new Action<int>(OnEntityUntracked));

            //var x = Globals.GetGlobal("Testing");
            //_log.Debug("Global found testing " + x);
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

    }
}
