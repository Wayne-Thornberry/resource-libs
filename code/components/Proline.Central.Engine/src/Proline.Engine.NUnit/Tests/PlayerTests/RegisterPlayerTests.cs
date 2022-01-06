using NUnit.Framework;
using Proline.Online.Data;
 
using Proline.CentralEngine.MidLayer;
using Proline.CentralEngine.NUnit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.NUnit.Tests.PlayerTests
{
    [TestFixture]
    public class RegisterPlayerTests
    {
        [Test]
        public void RegisterPlayerNewIdentity_ReturnsSuccess()
        {

            var identifiers = IdentityHelper.CreatePlayerIdentifiers(
                new[] { IdentityHelper.GenerateIdentifier(0), IdentityHelper.GenerateIdentifier(1), IdentityHelper.GenerateIdentifier(2), IdentityHelper.GenerateIdentifier(3) },
                new[] { 0, 1, 2, 3 });

            var rc = Engine.RegisterPlayer(Util.GenerateRandomString(25), identifiers);

            Assert.AreEqual(ReturnCode.Success, rc);
        }

        [Test]
        public void RegisterPlayerExistingIdentity_ReturnsFailure()
        {
            var identifiers = IdentityHelper.CreatePlayerIdentifiers(
                new[] { "X", "Y", "Z", "A" },
                new[] { 0, 1, 2, 3 });

            var rc = Engine.RegisterPlayer("ReservedUser", identifiers);

            Assert.AreNotEqual(ReturnCode.Success, rc);
        }

        [Test]
        public void RegisterPlayerExistingIdentity_ReturnsSuccess()
        {
            var identifiers = IdentityHelper.CreatePlayerIdentifiers(
                new[] { IdentityHelper.GenerateIdentifier((int)IdentifierType.SOCIAL), "Y", "Z", "A" },
                new[] { 0, 1, 2, 3 });

            var rc = Engine.RegisterPlayer("ReservedUser", identifiers);


            Assert.AreEqual(ReturnCode.Success, rc);
        }
    }
}
