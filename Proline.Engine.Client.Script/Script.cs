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
                    Type type = typeof(ClientEngineService);
                    MethodInfo info = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    object value = info.Invoke(null, new object[] { API.GetCurrentResourceName(), Game.Player.ServerId.ToString(), true });

                    //EventHandlers.Add(EngineConstraints.ExecuteEngineMethodHandler, new Action<int, string, string, string>(_service.RequestResponseHandler));
                    //await _service.Start(API.GetCurrentResourceName(), Game.Player.ServerId.ToString(), "true");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                Tick -= OnTick;
            }
        }


        //[Command("StartScript")]
        //public void StartScript(string[] args)
        //{
        //    var sn = args[0];
        //    //_service.StartScript(sn);
        //}

        //public void AddTick(Func<Task> task)
        //{
        //    Tick += task;
        //}
        //public void RemoveTick(Func<Task> task)
        //{
        //    Tick -= task;
        //}

        //public object GetGlobal(string key)
        //{
        //    return GlobalState.Get(key);
        //}

        //public void SetGlobal(string key, object data, bool replicated)
        //{
        //    GlobalState.Set(key, data, replicated);
        //}

        //#region Game Event
        //[EventHandler("gameEventTriggered")]
        //private void GameEventTriggered()
        //{
        //    //if (Core.Environment.IsGameStarted)
        //    //{ 
        //    //    //EngineEvents.TriggerGameEvent();
        //    //}
        //}
        //#endregion

        //#region Networking

        //[EventHandler(EngineConstraints.PushHandler)]
        //public void PushListener(string componentName, string propertyName, string data)
        //{


        //}
        //#endregion

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
        //#endregion
    }
}
