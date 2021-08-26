using System;

using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Engine.Networking;
using Proline.Engine;
using CitizenFX.Core;

namespace Proline.Online.Script
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


        [EventHandler(EngineConstraints.NetworkResponseListenerHandler)]
        public void NetworkResponseListener([FromSource] Player player, string guid, string value, bool isException)
        {
            EngineService.ExecuteEngineMethod("CreateAndInsertResponse", guid, value, isException);
        }

        [EventHandler(EngineConstraints.NetworkRequestListenerHandler)]
        public void NetworkRequestListener([FromSource] Player player, string guid, string methodName, string methodArgs)
        {
            var args = JsonConvert.DeserializeObject<object[]>(methodArgs);
            object result = null;
            bool isException = false;
            try
            {
                result = EngineService.ExecuteEngineMethod(methodName, args);
                if (result != null)
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
                TriggerClientEvent(player, NetworkManager.NetworkResponseListenerHandler, guid, result, isException);
            }
        }


        //[EventHandler(NetworkManager.NetworkRequestListenerHandle)]
        //public void NetworkRequestListener([FromSource] Player player, string guid, string componentName, string methodName, string methodArgs)
        //{
        //    var args = JsonConvert.DeserializeObject<object[]>(methodArgs);
        //    object result = null;
        //    bool isException = false;
        //    try
        //    {
        //        result = EngineService.ExecuteComponentControl(componentName, methodName, args);
        //        if (result != null)
        //        {
        //            if (!result.GetType().IsPrimitive)
        //            {
        //                result = JsonConvert.SerializeObject(result);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debugger.LogError(e.ToString());
        //        isException = true;
        //        throw;
        //    }
        //    finally
        //    {
        //        TriggerClientEvent(player, NetworkManager.NetworkResponseListenerHandler, guid, result, isException);
        //    }
        //}


        //[EventHandler(NetworkManager.NetworkResponseListenerHandler)]
        //public void NetworkResponseListener([FromSource] Player player, string eventParams)
        //{
        //    Debugger.LogDebug(eventParams.ToString());
        //    var instance = NetworkManager.GetInstance();
        //    var requestParams = JsonConvert.DeserializeObject<EventResponseParams>(eventParams);
        //    instance.CreateAndInsertResponse(requestParams.GUID, requestParams.Value, requestParams.IsException);
        //}

        //[EventHandler(NetworkManager.NetworkRequestListenerHandle)]
        //public void NetworkRequestListener([FromSource] Player player, string eventParams)
        //{
        //    Debugger.LogDebug(eventParams.ToString());
        //    var requestParams = JsonConvert.DeserializeObject<EventRequestParams>(eventParams);
        //    var args = JsonConvert.DeserializeObject<object[]>(requestParams.MethodArgs);
        //    object result = null;
        //    bool isException = false;
        //    try
        //    {
        //        result = EngineService.ExecuteComponentControl(requestParams.ComponentName, requestParams.MethodName, args);
        //        if (result != null)
        //        {
        //            if (!result.GetType().IsPrimitive)
        //            {
        //                result = JsonConvert.SerializeObject(result);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debugger.LogError(e.ToString());
        //        isException = true;
        //        throw;
        //    }
        //    finally
        //    {
        //        TriggerClientEvent(player, NetworkManager.NetworkResponseListenerHandler, JsonConvert.SerializeObject(new EventResponseParams()
        //        {
        //            GUID = requestParams.GUID,
        //            Value = result,
        //            IsException = isException
        //        }));
        //    }
        //}

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
            //deferrals.defer();

            //// mandatory wait!
            //await Delay(0);

            //var licenseIdentifier = player.Identifiers["license"];

            ////_service.LogDebug(_header, $"A player with the name {playerName} (Identifier: [{licenseIdentifier}]) is connecting to the server.");

            //deferrals.update($"Hello {playerName}, your license [{licenseIdentifier}] is being checked");

            //// Check if the connecting client (Player) has a matching identifier with any identifier passed
            //// If a match is found, fetch the user identity and prepare to tie this player 
            //// If over 50% of the identiies match from this player and what we have in the DB, use the existing identity row
            //// Fetch the player profile if one exists
            //// Search the user table for any active bans
            //// If one exists, check if its a global ban, if true, defer connection on the bases of a global ban
            //// If false, check if the instance id matches the instance id stored in a data object here
            //// If the user has no bans, check if the player has a ban
            //// if true, check if its a global ban.... same as above
            //// If everything checks out, continue connection

            //// Priority, if the server has a queue enabled, prepare to do some queue stuff
            //// This involves looking at the user and player priority.
            //// On a per instance level, instances have a table for player priority, while users have a priority Id
            //// We get both user and player priorty (if one exists) and take the highest of the two numbers 
            //// Thats your priority into the queue, the higher the number, the better the postition in queue

            //if (true)
            //{
            //    //deferrals.done("Rejected..");
            //}

            //deferrals.done();
        } 

        [EventHandler("playerDropped")]
        private async void OnPlayerDropped([FromSource] Player player, string reason)
        {

        }
        #endregion
    }
}
