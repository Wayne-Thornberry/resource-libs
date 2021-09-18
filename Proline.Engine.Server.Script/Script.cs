using System;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Proline.Engine.Script
{
    public class Script : BaseScript
    { 
        private bool _started;

        public Script()
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
                    string methodName = "Main";
                    Type type = typeof(ServerEngineService);
                    MethodInfo info = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    object value = info.Invoke(null, new object[] { API.GetCurrentResourceName(), -1, true });
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


        //#region FiveM Events
        //[EventHandler("onResourceStop")]
        //private void OnResourceStop(string resourceName)
        //{
        //    if (resourceName.Equals(API.GetCurrentResourceName()))
        //    {

        //    }
        //}

        //[EventHandler("onResourceStart")]
        //private void OnResourceStart(string resourceName)
        //{
        //    ////
        //    ///Handles setup engine startup
        //    ///Application Enviroment Startup

        //    if (resourceName.Equals(API.GetCurrentResourceName()))
        //    {

        //    }
        //}

        //[EventHandler("onResourceStarting")]
        //private void OnResourceStarting(string resourceName)
        //{
        //    ////
        //    ///Handles setup pre engine start
        //    ///Application Enviroment Startup
        //    if (resourceName.Equals(API.GetCurrentResourceName()))
        //    {
        //        //var envSetup = new Service();
        //        //envSetup.SetupEnviroment(1);
        //        //envSetup.SetupEngine();
        //        //envSetup.SetupGame(resourceName);
        //    }
        //}

        //[EventHandler("playerConnecting")]
        //private async void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        //{
        //    _service.PlayerConnectingHandler(player, playerName, setKickReason, deferrals);
        //}

        //[EventHandler("playerDropped")]
        //private async void OnPlayerDropped([FromSource] Player player, string reason)
        //{ 
        //    _service.PlayerDroppedHandler(player, reason);
        //}
        //#endregion

    }
}
