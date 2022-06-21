using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Tests.InstanceTests
{
    [TestFixture]
    public class JoinInstanceTests
    {
        [Test]
        public void JoinInstance_ReturnSuccess()
        {
            //InstanceHelper.SetupInstanceWithPlayers(2, out var playerIds, out var userIds, out var instanceId);

            //PlayerHelper.CreateNewPlayerWithNewUser(out var playerId, out var userId);

            //var rc = Engine.AddPlayerToInstance(playerId, instanceId);

            //Assert.AreEqual(ReturnCode.Success, rc);

            //rc = Engine.GetInstancePlayers(out var players);

        }

        [Test]
        public void JoinInstanceUserIsBanned_ReturnFailure()
        {
            //InstanceHelper.SetupInstanceWithPlayers(2, out var playerIds, out var userIds, out var instanceId);

            //PlayerHelper.CreateNewPlayerWithNewUser(out var playerId, out var userId);

            //var rc = Engine.DenyUser(userId, out var den);

            //rc = Engine.AddPlayerToInstance(playerId, instanceId);

            //Assert.AreNotEqual(ReturnCode.Success, rc);  

        }
    }
}
