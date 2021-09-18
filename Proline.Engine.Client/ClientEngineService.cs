


using Newtonsoft.Json;
using Proline.Engine.Componentry;
using Proline.Engine.Data;
using Proline.Engine.Networking;
using Proline.Engine.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Proline.Engine.Extension;
using Proline.Engine.Internals;
using Proline.Engine.Resource;
using CitizenFX.Core.Native;

namespace Proline.Engine
{
    public class ClientEngineService : EngineService
    {  
        public static void Main(string[] args)
        {
            var service = new ClientEngineService();
            var resourceName = args[0];
            var sourceHandle = int.Parse(args[1]);
            var isDebug = bool.Parse(args[2]);
            var isIsolated = resourceName.Equals("ConsoleApp");
            EngineConfiguration.IsClient = sourceHandle != -1;
            EngineConfiguration.IsIsolated = isIsolated;
            EngineConfiguration.IsDebugEnabled = isDebug;
            EngineConfiguration.OwnerHandle = sourceHandle;
            EngineConfiguration.EnvTypeName = (EngineConfiguration.IsClient ? "Client" : "Server");
            service.Initialize(); 
        }


        protected override void LoadConfig(bool isDebugEnabled)
        {
            var resourceName = API.GetCurrentResourceName();
            var configJson = API.LoadResourceFile(resourceName, "Engine.json");
            EngineConfiguration.LoadConfig(configJson);
            EngineConfiguration.IsDebugEnabled = isDebugEnabled;
        }
      
     

       // protected abstract Task ExecuteEnvFunctions(); 

        public void PlayerConnectingHandler(object player, string playerName, dynamic setKickReason, dynamic deferrals)
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

       


     
         
    }
}
