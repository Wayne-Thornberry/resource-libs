using NUnit.Framework;
using Proline.Online.Data;
 
using Proline.CentralEngine.MidLayer;
using System;
using System.Linq;

namespace Proline.CentralEngine.NUnit.Tests.UserTests
{
    [TestFixture]
    public class CreateUserTests
    {
        [Test]
        public void CreateNewUser_ReturnsSuccess()
        {
            var username = Util.GenerateRandomString(28);
            var rc = Engine.RegisterUser(username);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.GetUserDetails(username, out var userDetails);

            Assert.AreEqual(ReturnCode.Success, rc);
            Assert.IsNotNull(userDetails);
        }

        [Test]
        public void CreateNewUserFromExistingUsername_ReturnsError()
        {
            var username = "ReservedUser"; 

            var rc = Engine.RegisterUser(username);
            Assert.AreNotEqual(ReturnCode.Success, rc);

            rc = Engine.GetUserDetails(username, out var userDetails);

            Assert.AreEqual(ReturnCode.Success, rc);
            Assert.IsNotNull(userDetails);
        } 
    }
}
