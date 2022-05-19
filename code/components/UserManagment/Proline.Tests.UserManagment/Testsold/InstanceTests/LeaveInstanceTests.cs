using NUnit.Framework;
using Proline.Online.Data;

using Proline.CentralEngine.MidLayer;
using Proline.CentralEngine.NUnit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.NUnit.Tests.InstanceTests
{
    [TestFixture]
    public class LeaveInstanceTests
    {
        [Test]
        public void LeaveInstanceTest_ReturnSuccess()
        {
            InstanceHelper.SetupInstanceWithPlayers(2, out var playerIds, out var userIds, out var instanceId); 

            var rc = Engine.RemovePlayerFromInstance(playerIds[0], instanceId);

            Assert.AreEqual(ReturnCode.Success, rc);

        }
    }
}
