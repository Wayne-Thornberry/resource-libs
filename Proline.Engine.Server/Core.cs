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

                    await _service.Start(API.GetCurrentResourceName(), "-1", "true");

                }
                //EngineService.ExecuteEngineMethod("ExecuteComponentControl", "", "4392749", JsonConvert.SerializeObject(new object[] { "Testo8717" }));
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
