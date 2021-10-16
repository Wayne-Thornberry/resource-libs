using Proline.Online.Data;
using Proline.CentralEngine.DBApi; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.CentralEngine.DBApi.Models.Central;

namespace Proline.CentralEngine.MidLayer
{
    public static partial class Engine
    {
        public static ReturnCode RegisterInstance(string instanceName, long instanceIdentityId)
        {
            return RegisterInstance(new RegisterInstanceInParameter() { InstanceName = instanceName, InstanceIdentityId = instanceIdentityId });
        }

        public static ReturnCode GetInstanceDetails(long instanceIdentityId, out InstanceDetailsOutParameter outParameters)
        {
            var connection = new DBConnection();
            var instanceIdentity = connection.GetInstanceIdentity(instanceIdentityId, true);
            outParameters = null;

            var instance = connection.GetInstance(instanceIdentity.InstanceIdentityId, false);
            if (instance == null)
                return ReturnCode.SystemError;
            outParameters = new InstanceDetailsOutParameter
            {
                InstanceId = instance.InstanceId,
            };
            return ReturnCode.Success;
        }
        public static ReturnCode StartInstance(long instanceId)
        {
            var connection = new DBConnection();
            var instance = connection.GetInstance(instanceId);
            var identity = connection.GetInstanceIdentity(instance.InstanceIdentityId, false);
            var denial = connection.GetDenial(identity.UserId);
            if (denial != null)
                return ReturnCode.InstanceFailedUserDenied;

            if (string.IsNullOrEmpty(instance.Type) || instance.Type.Equals("Offline"))
            { 
                instance.Type = "Online";
                connection.UpdateInstance(instance);
            } 

            return ReturnCode.Success;
        }

        public static ReturnCode StopInstance(long instanceId)
        {
            var connection = new DBConnection();
            var instance = connection.GetInstance(instanceId);
            if (string.IsNullOrEmpty(instance.Type))
                return ReturnCode.Success;

            if (instance.Type.Equals("Online"))
            { 
                instance.Type = "Offline";
                connection.UpdateInstance(instance);
            }

            return ReturnCode.Success;
        }

        public static ReturnCode RegisterInstance(RegisterInstanceInParameter inParameters)
        {
            var connection = new DBConnection();
            var instanceIdentity = connection.GetInstanceIdentity(inParameters.InstanceIdentityId, true);
            Instance instance = null;
            var denial = connection.GetDenial(instanceIdentity.UserId);
            if (denial != null)
                return ReturnCode.InstanceFailedUserDenied;
            if (instanceIdentity != null)
            {
                instance = connection.GetInstance(inParameters.InstanceName);
                if(instance == null)
                {

                    instance = new Instance()
                    {
                        Name = inParameters.InstanceName,
                        InstanceIdentityId = instanceIdentity.InstanceIdentityId,
                        MaxPlayers = 32,
                    };
                    instance = connection.InsertInstance(instance);
                }
            }
            instance.LastSeenOnlineAt = DateTime.UtcNow;
            connection.UpdateInstance(instance);
            return ReturnCode.Success;
        }

        public static ReturnCode GetInstancePlayers(long instanceId, out IEnumerable<InstancePlayer> players)
        {
            var connection = new DBConnection();
            players = connection.GetInstancePlayers(instanceId); 
            return ReturnCode.Success;
        }

        public static ReturnCode AddPlayerToInstance(long playerId, long instanceId)
        {
            var connection = new DBConnection();
            var inc = connection.GetInstance(instanceId);
            if(DateTime.UtcNow.Subtract(inc.LastSeenOnlineAt).TotalMinutes > 1)
            {
                // check if the instance is alive 
                return ReturnCode.SystemError;
            }
            var identities = connection.GetIdentities(playerId).ToArray();
            var identity = identities[0];

            var user = connection.GetUser(identity.UserId);

            // check if the player is not on the instance ban list
            var deny = connection.GetInstanceDeny(user.UserId);
            if (deny != null)
                return ReturnCode.SystemError;

            // check if the user is not on the deny list
            var de = connection.GetDenial(user.UserId);
            if (de != null)
                return ReturnCode.SystemError; 

            var ip = connection.GetInstancePlayer(playerId);
            if (ip != null)
                return ReturnCode.SystemError;

            connection.InsertInstancePlayer(new InstancePlayer() { PlayerId = playerId, InstanceId = instanceId, LastSeenAt = DateTime.UtcNow });

            return ReturnCode.Success;
        }

        public static ReturnCode RemovePlayerFromInstance(long playerId, long instanceId)
        {
            var connection = new DBConnection(); 
            var ip = connection.GetInstancePlayer(playerId);
            if (ip == null)
                return ReturnCode.SystemError;
            connection.DeleteInstancePlayer(ip.InstancePlayerId);
            return ReturnCode.Success;
        } 


        public static ReturnCode InstanceHeartbeat(Instance instance)
        {
            //// TODO
            /// if the player list we get has users that are missing from the table, but on the player list, they should be kicked
            /// if the plsyer list table has players that dont exist on the player list itself, remove the player from the instance player table
            return ReturnCode.Success;
        }

        public static ReturnCode GenerateInstanceKey(long userId, out long instanceIdentityId)
        {
            var connection = new DBConnection();
            var instanceIdentity = connection.GetInstanceIdentity(userId);
            instanceIdentityId = 0;
            if (instanceIdentity == null)
            {
                var guid = Guid.NewGuid().ToString();
                instanceIdentity = new UserInstanceLicence() { UserId = userId, Key = guid };
                connection.InsertInstanceIdentity(instanceIdentity);
                instanceIdentityId = instanceIdentity.InstanceIdentityId;
            }
            else
                return ReturnCode.InstanceKeyGenFailedUserHasKey;
            return ReturnCode.Success;
        }
    }
}
