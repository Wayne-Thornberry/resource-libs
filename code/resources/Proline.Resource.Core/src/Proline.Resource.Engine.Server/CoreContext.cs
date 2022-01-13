using CitizenFX.Core;
using Proline.Component.UserManagment;
using Proline.Resource.Core.Access;
using Proline.Resource.Framework.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Proline.Component.CScripting.Server
{
    public class CoreContext : ComponentContext
    {
        private AuthenticationHeaderValue _authHeader;

        public override void OnLoad()
        {
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        }

        [EventHandler("playerConnecting")]
        public void OnPlayerConnecting([FromSource] Player player, string playerName, object setKickReason, dynamic deferrals)
        {
            //using (EngineAPI access =  new EngineAPI())
            //{
            //    deferrals.defer();
            //    deferrals.update("Checking player infromation");
            //    access.ProcessPlayerConnection(playerName, identities, out var userAccount, out var userDeny, out var playerAccount, out var playerDeny);
            //    long userId = 0;
            //    var licence = player.Identifiers["licence"];
            //    var identity = access.GetPlayerIdentity(licence);
            //    if (identity == null)
            //    {
            //        // Finding out if the other identities match
            //        var identities = new List<IdentifierInParameter>
            //    {
            //        new IdentifierInParameter()
            //        {
            //            Identifier = player.Identifiers["steam"],
            //            IdentitierType = 0,
            //        },
            //        new IdentifierInParameter()
            //        {
            //            Identifier = player.Identifiers["ip"],
            //            IdentitierType = 1,
            //        },
            //        new IdentifierInParameter()
            //        {
            //            Identifier = player.Identifiers["discord"],
            //            IdentitierType = 2,
            //        }
            //    };
            //        var ids = access.GetPlayerIdentities(identities);
            //        if (ids.Count() > 0)
            //        {
            //            userId = ids.FirstOrDefault().UserId;
            //            // then we need to decide as we found these identities tied to user accounts
            //            access.PutPlayerIdentity(new IdentifierInParameter()
            //            {
            //                Identifier = player.Identifiers["licence"],
            //                IdentitierType = 4,
            //            });
            //        }
            //        else
            //        {
            //            // this is a new player, none of the identities matches up to this point
            //            identities.Add(new IdentifierInParameter()
            //            {
            //                Identifier = player.Identifiers["licence"],
            //                IdentitierType = 4,
            //            });

            //            access.CreateNewUser(new UserAccountInParameter()
            //            {
            //                Username = player.Name,
            //                Identifiers = identities
            //            });
            //        }
            //    }
            //    else
            //    {
            //        userId = identity.UserId;
            //    }

            //    var result = access.GetUserAccount(userId);
            //    var deny = access.GetUserDeny(result.UserId);
            //    if (access.IsUserDenied(deny))
            //    {
            //        deferrals.done("Player is banned");
            //        return;
            //    }

            //    deferrals.done();
            //}
           
        }

       
    }
}
