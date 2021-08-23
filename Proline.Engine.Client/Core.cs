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
                        await _service.Initialize(API.GetCurrentResourceName(), Game.PlayerPed.Handle.ToString(), "true");
                        _service.StartAllComponents();
                        _service.StartStartupScripts();
                        //Tick += _service.Tick;
                    }
                    //API.RegisterCommand("ecc", new Action<int, List<object>, string>((source, args, rawCommand) =>
                    //{
                    //    var component = args[0].ToString();
                    //    var method = args[1].ToString();
                    //    args.Remove(args[0]);
                    //    args.Remove(args[0]);
                    //    Debugger.LogDebug(component);
                    //    Debugger.LogDebug(method);
                    //    var param = args.ToArray();
                    //    EngineService.ExecuteComponentCommand(component, method, param);
                    //}), false);

                    //var x = "public enum EngineAPI\n" +
                    //    "{\n";
                    //var apis = EngineAccess.GetAllAPIs();
                    //foreach (var item in apis)
                    //{
                    //    x += "  " + item + ",\n";
                    //}
                    //x += "\n" +
                    //    "}";
                    //Debugger.LogDebug(x, true);
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
