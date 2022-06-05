using NUnit.Framework;
using Proline.CentralEngine.NUnit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.NUnit.Tests.InstanceTests
{
    [TestFixture]
    public class StartInstanceTests
    {
        [Test]
        public void StartNewInstance_ReturnSuccess()
        {
            //UserHelper.SetupUser(out var userId);

            //var rc = Engine.GenerateInstanceKey(userId, out long instanceIdentity);

            //rc = Engine.RegisterInstance(Util.GenerateRandomString(10), instanceIdentity);

            //rc = Engine.GetInstanceDetails(instanceIdentity, out var details);

            //rc = Engine.StartInstance(details.InstanceId);

            //Assert.AreEqual(ReturnCode.Success, rc);
        }

        [Test]
        public void RegisterNewInstanceUserDenied_ReturnFailure()
        {
            //UserHelper.SetupUser(out var userId);

            //var rc = Engine.GenerateInstanceKey(userId, out var instanceIdentity);

            //Engine.DenyUser(userId, out var denial);

            //rc = Engine.RegisterInstance(Util.GenerateRandomString(10), instanceIdentity);
            //Assert.AreNotEqual(ReturnCode.Success, rc);
        }

        [Test]
        public void GenerateInstanceKey_ReturnSuccess()
        {
            //var username = Util.GenerateRandomString(28);
            //var rc = Engine.RegisterUser(username);
            //Assert.AreEqual(ReturnCode.Success, rc);

            //rc = Engine.GetUserDetails(username, out var userDetails);

            //rc = Engine.GenerateInstanceKey(userDetails.UserId, out long instanceIdentity);

            //Assert.AreEqual(ReturnCode.Success, rc);
            //Assert.IsNotNull(instanceIdentity);
        }

        [Test]
        public void GenerateInstanceKeyExistingUser_ReturnFailure()
        {
            //var username = "Registered User";
            //var rc = Engine.RegisterUser(username);
            //Assert.AreEqual(ReturnCode.UserAlreadyExists, rc);

            //rc = Engine.GetUserDetails(username, out var userDetails); 

            //rc = Engine.GenerateInstanceKey(userDetails.UserId, out var instanceIdentity);

            //Assert.AreEqual(ReturnCode.InstanceKeyGenFailedUserHasKey, rc);
            //Assert.IsNotNull(instanceIdentity);
        }
    }
}
