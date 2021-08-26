using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Engine;

namespace Proline.Engine.Script
{
    public partial class EngineScript : BaseScript, IScriptSource
    {
        private bool _started;
        private EngineService _service;

        public EngineScript()
        {
            _service = new EngineService(this);
            Tick += OnTick;
        } 

        private async Task OnTick()
        {
            try
            {

                if (!_started)
                {
                    _started = true; 
                    await _service.Start(API.GetCurrentResourceName(), Game.PlayerPed.Handle.ToString(), "true");
                    _service.StartAllComponents();
                    _service.StartStartupScripts();
                }
            }
            catch (Exception)
            {
                Tick -= OnTick; 
                throw;
            } 
        }

      
    }
}
