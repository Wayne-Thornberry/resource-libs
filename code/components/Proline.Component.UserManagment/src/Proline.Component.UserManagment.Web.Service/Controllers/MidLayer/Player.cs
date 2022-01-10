using Proline.Online.Data;
using Proline.CentralEngine.DBApi;
using Proline.CentralEngine.DBApi.Models.Central;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Component.Engine.Web.Service
{
    public static partial class Engine
    {

        public static ReturnCode GetPlayerIdentitiesFromIdentifiers(string[] identifiers, out PlayerIndentity[] registrations)
        {
            var ids = new List<PlayerIndentity>();
            registrations = null;
            try
            {
                var connection = new DBConnection();

                for (int i = 0; i < identifiers.Length; i++)
                {
                    var idCard = connection.GetIdentity(identifiers[i]);
                    if (idCard != null)
                        ids.Add(idCard);
                }
                registrations = ids.ToArray();
            }
            catch (Exception e)
            {
                return ReturnCode.SystemError;
                throw;
            }
            return ReturnCode.Success;
        }

        public static ReturnCode GetPlayerDetails(long playerId, out PlayerDetailsOutParameter playerDetails)
        {
            var connection = new DBConnection();
            var player = connection.GetPlayerRegistration(playerId);
            playerDetails = new PlayerDetailsOutParameter
            {
                PlayerId = player.PlayerId,
            };
            return ReturnCode.Success;
        }

        public static ReturnCode GetPlayerDetails(string[] identifiers, out PlayerDetailsOutParameter playerDetails)
        {
            var scId = identifiers[0];
            var connection = new DBConnection();
            var identity = connection.GetIdentity(scId);
            var player = connection.GetPlayerRegistration(identity.PlayerId);
            playerDetails = new PlayerDetailsOutParameter
            {
                PlayerId = player.PlayerId,
            };
            return ReturnCode.Success;
        }

        public static ReturnCode GetPlayerIdentities(long playerId, out PlayerIndentity[] identities)
        {
            identities = null;
            try
            {
                var connection = new DBConnection();
                identities = connection.GetIdentities(playerId).ToArray();
            }
            catch (Exception e)
            {
                return ReturnCode.SystemError;
                throw;
            }
            return ReturnCode.Success;
        }

        public static ReturnCode RegisterIdentifiersToPlayerAndUser(string[] identifiers, long playerId, long userId, out PlayerIndentity[] ids)
        {
            ids = new PlayerIndentity[identifiers.Length];
            for (int i = 0; i < identifiers.Length; i++)
            {
                var rc = RegisterIdentifierToPlayer(identifiers[i], playerId, userId, out ids[i]);
                //if (rc != ReturnCode.Success come back here
                //    return rc;
            }
            return ReturnCode.Success;
        }

        public static ReturnCode RegisterIdentifierToPlayer(string identifier, long playerId, long userId, out PlayerIndentity idenity)
        {
            var connection = new DBConnection();
            idenity = connection.GetIdentity(identifier);
            if (idenity == null && !string.IsNullOrEmpty(identifier))
            {
                try
                {
                    idenity = new PlayerIndentity()
                    {
                        Identifier = identifier,
                        PlayerId = playerId,
                        UserId = userId,
                    };
                    connection.InsertPlayerIdentity(idenity);
                }
                catch (Exception)
                {
                    return ReturnCode.IdentityFailedInsertion;
                    throw;
                }
            }
            else
            {
                return ReturnCode.IdentityAlreadyExists;
            }
            return ReturnCode.Success;
        }

        /// <summary>
        /// Attempts to register a playe with given parameters and identities, if all identities match, a duplicate player return code is returned
        /// if the player SC id is not matched but other identities match, a new player is created and tied to the existing user
        /// if no identities match, a new user is created with a new player, joining user and player
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="identifiers"></param>
        /// <param name="player"></param>
        /// <param name="ids"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ReturnCode RegisterPlayer(string playerName, string[] identifiers, long userId = 0)
        { 
            var connection = new DBConnection();
            try
            {
                var rc = GetPlayerIdentitiesFromIdentifiers(identifiers, out var ids);
                // if the player has no matching identities
                if (ids.Length == 0)
                {
                    // this must be a new player and user
                    rc = RegisterUser(playerName);

                    rc = GetUserDetails(playerName, out var detailsOutParameter);

                    userId = detailsOutParameter.UserId;

                }
                else
                {
                    // if the player does have identities, then get the user profile
                    var identity = ids[0];

                    var user = connection.GetUser(identity.UserId);

                    // if we find that the first identity, I.E social club id, matches the passed social club id, then i'm pretty confident this is the same player
                    if (identity.Identifier.Equals(identifiers[0]))
                    { 
                        ids = null;
                        return ReturnCode.PlayerAlreadyExists;
                    }
                    else
                    {
                        // if the first identity does not match, then it must be a new player but we found other
                        // identifiers that match, assumed to be an alt 
                    }
                }


                var player = new PlayerAccount()
                {
                    Name = playerName,
                    Priority = 0,
                    RegisteredAt = DateTime.UtcNow
                };


                player = connection.InsertPlayerAccount(player);

                if (player == null)
                    return ReturnCode.PlayerCreationFailed;

                rc = RegisterIdentifiersToPlayerAndUser(identifiers, player.PlayerId, userId, out ids);
            }
            catch (Exception e)
            {
                return ReturnCode.SystemError;
                throw;
            }

            return ReturnCode.Success;
        }
    }
}
