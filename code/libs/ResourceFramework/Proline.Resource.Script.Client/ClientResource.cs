using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Common.Logging;
using Proline.Resource.Common; 
using Proline.Resource.CFX; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Proline.Resource.Component;
using Proline.Resource.Common.Component;
using Proline.Resource.Common.Script;
using Proline.Resource.Common.CFX;
using Proline.Resource.Script.CFX;

namespace Proline.Resource.Script
{
    public class ClientResource : BaseScript, IScriptSource
    {
        private bool _isSetup;
        private Log _log => Logger.GetInstance().GetLog();  
        private ComponentContainer _component; 

        public IFXConsole Console { get; }
        public IFXEventTrigger EventTrigger { get; }
        public IFXEventHandler EventHandler { get; }
        public IFXResource Resource { get; }
        public IFXTask Task { get; }

        public ClientResource()
        { 
            Tick += OnTick;
            Resource = new FXResource(API.GetCurrentResourceName());
            Task = new FXTask();
            EventHandler = new FXEventHandler(EventHandlers);
            EventTrigger = new FXEventTrigger();
            Console = new FXConsole();
            Logger.GetInstance().SetOutput(Console);
            ScriptSource.Source = this;
        }

        private async Task OnTick()
        {
            try
            {
                if (!_isSetup)
                { 
                    _component = new ComponentContainer(this);
                    var config =_component.LoadConfig();
                    _component.LoadEnviroment(config.Client, ComponentEnviromentType.CLIENT);
                    _isSetup = true; 
                }
            }
            catch (Exception)
            { 
                _isSetup = true;
                Tick -= OnTick;
                throw;
            }
        }  
    }
}
