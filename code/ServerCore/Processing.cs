using CitizenFX.Core; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore
{
    public static partial class API
    {
        //public static async Task PlayerDroppedProcessing(Player player, string reason)
        //{
        //    var _manager = PlayerManager.GetInstance();
        //    using (var x = new DBAccessClient())
        //    {
        //        var response = await x.GetPlayer(new GetPlayerRequest() { Username = player.Name });
        //        var ePlayer = _manager.GetPlayer(response.PlayerId);
        //        _manager.RemovePlayer(ePlayer);
        //    }
        //}

        //public static async Task PlayerConnectionValidator(Player player, dynamic deferrals)
        //{
        //    var _manager = PlayerManager.GetInstance();
        //    deferrals.update("Checking player infromation");
        //    long playerId = 0;
        //    using (var x = new DBAccessClient())
        //    {
        //        var response = await x.GetPlayer(new GetPlayerRequest() { Username = player.Name });
        //        if (response.ReturnCode != 0)
        //        {
        //            var y = await x.RegisterPlayer(new RegisterPlayerRequest() { Name = player.Name });
        //            if (y.ReturnCode != 0)
        //            {
        //                deferrals.done("Internal Server error, Please contact Server Admins");
        //                return;
        //            }
        //            playerId = y.Id;
        //        }
        //        else
        //        {
        //            // check if the player is banned and deal with it
        //            playerId = response.PlayerId;
        //        }
        //    }
        //    var ePlayer = new ExtendedPlayer() { Player = player, PlayerId = playerId };
        //    _manager.AddPlayer(ePlayer);
        //    deferrals.done();
        //}
    }
}
