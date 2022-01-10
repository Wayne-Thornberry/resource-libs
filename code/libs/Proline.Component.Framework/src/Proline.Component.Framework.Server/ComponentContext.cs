using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Component.Framework.Client.Access; 
using Proline.Resource.Framework.Server.Eventing;
using Proline.Resource.Framework.Server.Logging;
using Proline.Resource.Framework.Server.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework.Server
{
    public abstract class ComponentContext : BaseScript
    {
        protected Log _log;
        public string Name { get; internal set; }
        public virtual void OnLoad() { }
        public virtual void OnStart() { }
        public virtual async Task OnTick() { }

        public ComponentContext()
        {
            EventManager.SetHandlerCollection(EventHandlers);
            ExportManager.SetExportDictionary(Exports);
            MemoryManager.SetGlobalBag(GlobalState);
            _log = Logger.GetInstance().GetLog();
            Tick += InternalOnTick;
        }
          
        private bool _isSetup;  
        private async Task InternalOnTick()
        {
            try
            { 
                if (!_isSetup)
                { 
                    OnLoad();
                    OnStart();
                    _isSetup = true;
                }
                await OnTick();
            }
            catch (Exception)
            {
                Tick -= InternalOnTick;
                throw;
            }
        }

    }
}
