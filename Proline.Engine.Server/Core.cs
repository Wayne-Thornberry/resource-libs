using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Engine;

namespace Proline.Online.Script
{
    public partial class EngineScript : BaseScript, IScriptSource
    { 
        private bool _started;
        private EngineService _service;

        public EngineScript()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            try
            {
                if (!_started)
                {
                    _started = true;

                    if (_service == null)
                    {
                        _service = new EngineService(this);
                        _service.Initialize(API.GetCurrentResourceName(), "-1", "true");
                        _service.StartAllComponents();
                        _service.StartStartupScripts();
                    }

                }
                EngineService.ExecuteEngineMethod("ExecuteComponentControl", "", "4392749", JsonConvert.SerializeObject(new object[] { "Testo8717" }));
            }
            catch (Exception)
            { 
                throw;
            }
            finally
            { 
                Tick -= OnTick;
            }
        }
    }
}
