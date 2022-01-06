using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Component.Framework.Client.Access;
using Proline.Component.Framework.Client.Global;
using Proline.Resource.Client.Component;
using Proline.Resource.Client.Logging;
using Proline.Resource.Component.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Framework
{
    public abstract class ComponentContext : BaseScript
    {
        protected Log _log => Logger.GetInstance().GetLog();
        public string Name { get; internal set; }

        public virtual void OnLoad() { }
        public virtual void OnStart() { }
        public virtual async Task OnTick() { }

        public ComponentContext()
        {
            EventManager.SetHandlerCollection(EventHandlers);
            ExportManager.SetExportDictionary(Exports);
            Globals.SetGlobalBag(GlobalState); 
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
                }
                await OnTick();
            }
            catch (Exception)
            {
                Tick -= InternalOnTick;
                throw;
            }
            finally
            {
                _isSetup = true;
                //Tick -= InternalOnTick;
            }
        }  
    }
}
