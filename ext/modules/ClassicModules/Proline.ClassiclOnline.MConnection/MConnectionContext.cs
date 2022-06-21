using CitizenFX.Core;
using Proline.Modularization.Core;
using System;
using System.Reflection;

namespace Proline.ClassicOnline.PMGT
{
    public class MConnectionContext : ModuleScript
    {
        public MConnectionContext(Assembly source) : base(source)
        {
        }

#if SERVER
        [EventHandler("playerConnecting")]
        public void OnPlayerConnecting([FromSource] Player player, string playerName, object setKickReason, dynamic deferrals)
        {

            //using (var x = new DBAccessClient())
            //{
            //    x.LoadFile(new GetPlayerDataInParameters());
            //}


            //deferrals.defer();
            //deferrals.update("Checking player infromation");
            //ReturnCode rc = ReturnCode.Success;

            //if (!PlayerAccount.IsPlayerRegistered(player))
            //{
            //    rc = PlayerAccount.TryRegisterPlayer(player);
            //    if (rc != ReturnCode.Success)
            //    { 
            //        deferrals.done("Internal Server error, Please contact Server Admins");
            //    }
            //}


            //deferrals.update("Retriving Account Details");
            //var details = PlayerAccount.GetPlayerProfile(player);
            //if(details == null)
            //{
            //    deferrals.done("Internal Server error, Please contact Server Admins"); 
            //}

            //deferrals.update("Logging in....");
            //rc = PlayerAccount.TryLoginPlayer(player);
            //if (rc == ReturnCode.Success)
            //{
            //    deferrals.done();
            //}
            //else if(rc == ReturnCode.LoginFailedPlayerDenied)
            //{
            //    if (PlayerAccount.IsPlayerDenied(player))
            //    {
            //        //PlayerAccount.
            //        deferrals.done("Player Banned");
            //    }
            //}
            //else
            //{ 
            //    deferrals.done("Unable to login player, please contact server admins");
            //} 
            //TriggerEvent("playerConnectedHandler", player.Handle);
        }
#endif
    }
}
