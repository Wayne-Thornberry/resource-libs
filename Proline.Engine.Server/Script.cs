extern alias Server;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proline.Engine;
using Server::CitizenFX.Core;
using Server::CitizenFX.Core.Native;

namespace Proline.Engine.Server
{
    public class Script : BaseScript, IScriptSource
    { 
        private bool _started;
        private EngineService _service;

        public Script()
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
                    EventHandlers.Add(EngineConstraints.ExecuteEngineMethodHandler, new Action<int, string, string, string>(_service.RequestResponseHandler));
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

        #region Game Event
        [EventHandler("gameEventTriggered")]
        private void GameEventTriggered()
        {
            //if (Core.Environment.IsGameStarted)
            //{ 
            //    //EngineEvents.TriggerGameEvent();
            //}
        }
        #endregion


        #region FiveM Events
        [EventHandler("onResourceStop")]
        private void OnResourceStop(string resourceName)
        {
            if (resourceName.Equals(API.GetCurrentResourceName()))
            {

            }
        }

        [EventHandler("onResourceStart")]
        private void OnResourceStart(string resourceName)
        {
            ////
            ///Handles setup engine startup
            ///Application Enviroment Startup

            if (resourceName.Equals(API.GetCurrentResourceName()))
            {

            }
        }

        [EventHandler("onResourceStarting")]
        private void OnResourceStarting(string resourceName)
        {
            ////
            ///Handles setup pre engine start
            ///Application Enviroment Startup
            if (resourceName.Equals(API.GetCurrentResourceName()))
            {
                //var envSetup = new Service();
                //envSetup.SetupEnviroment(1);
                //envSetup.SetupEngine();
                //envSetup.SetupGame(resourceName);
            }
        }

        [EventHandler("playerConnecting")]
        private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            _service.PlayerConnectingHandler(player, playerName, setKickReason, deferrals);
        }

        [EventHandler("playerDropped")]
        private async void OnPlayerDropped([FromSource] Player player, string reason)
        { 
            _service.PlayerDroppedHandler(player, reason);
        }
        #endregion

        public void AddTick(Func<Task> task)
        {
            Tick += task;
        }

        public object GetGlobal(string key)
        {
            throw new NotImplementedException();
        }


        public void RemoveTick(Func<Task> task)
        {
            Tick -= task;
        }

        public void SetGlobal(string key, object data, bool replicated)
        {
            throw new NotImplementedException();
        }

       

        public void Write(object data)
        {
            Debug.Write(data.ToString());
        }

        public void WriteLine(object data)
        {
            Debug.WriteLine(data.ToString());
            //string x = "";
            //if(File.Exists("Log"))
            //  x = File.ReadAllText("Log");
            //File.WriteAllText("Log", x + "\n" + data.ToString());
        }
    }
}
