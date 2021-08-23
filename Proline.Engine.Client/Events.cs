using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Engine; 
using Proline.Engine.Networking;

namespace Proline.Engine.Script
{
    public partial class EngineScript : BaseScript
    {
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

        #region Networking

        [EventHandler(NetworkManager.NetworkResponseListenerHandler)]
        public void NetworkResponseListener(string guid, string value, bool isException)
        { 
            EngineService.ExecuteEngineMethod("CreateAndInsertResponse", guid, value, isException);
        }

        [EventHandler(NetworkManager.NetworkRequestListenerHandle)]
        public void NetworkRequestListener(string guid, string componentName, string methodName, string methodArgs)
        { 
            var args = JsonConvert.DeserializeObject<object[]>(methodArgs);
            object result = null;
            bool isException = false;
            try
            { 
                result = EngineService.ExecuteEngineMethod(methodName, args);
                if(result != null)
                { 
                    if (!result.GetType().IsPrimitive)
                    {
                        result = JsonConvert.SerializeObject(result);
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.LogError(e.ToString());
                isException = true;
                throw;
            }
            finally
            {
                TriggerEvent(NetworkManager.NetworkResponseListenerHandler, guid, result, isException);
            }
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
        #endregion
    }
}
