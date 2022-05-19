using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.DBAccess.NUnit;

namespace Proline.DBAccess.NUnit.Helpersold
{
    public static class InstanceHelper
    {
        public static void SetupInstanceWithPlayers(int playerCount, out long[] playerIds, out long[] userIds, out long instanceId)
        {
            var hostUsername = Util.GenerateRandomString(28);
            var rc = Engine.RegisterUser(hostUsername);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.GetUserDetails(hostUsername, out var userDetails);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.GenerateInstanceKey(userDetails.UserId, out long instanceIdentity);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.RegisterInstance(Util.GenerateRandomString(10), instanceIdentity);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.GetInstanceDetails(instanceIdentity, out var details);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.StartInstance(details.InstanceId);

            playerIds = new long[playerCount];
            userIds = new long[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                PlayerHelper.CreateNewPlayerWithNewUser(out var playerId, out var userId);
                rc = Engine.AddPlayerToInstance(playerId, details.InstanceId);
                playerIds[i] = playerId;
                userIds[i] = userId;
                Assert.AreEqual(ReturnCode.Success, rc);
            }

            instanceId = details.InstanceId;
        }
    }
}
