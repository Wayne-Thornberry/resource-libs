using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Tests.UsersTests
{
    [TestFixture]
    public class AllowUserTests
    {
        [Test]
        public void AllowUser_ReturnsSuccess()
        {
            //    var username = Util.GenerateRandomString(28);
            //    var rc = Engine.RegisterUser(username);
            //    Assert.AreEqual(ReturnCode.Success, rc);

            //    rc = Engine.GetUserDetails(username, out var userDetails);

            //    rc = Engine.AllowUser(userDetails.UserId, out var allowId);

            //    Assert.AreEqual(ReturnCode.Success, rc); 
        }

        [Test]
        public void AllowUserTwice_ReturnsFailure()
        {

            //    var username = Util.GenerateRandomString(28);
            //    var rc = Engine.RegisterUser(username);
            //    Assert.AreEqual(ReturnCode.Success, rc);

            //    rc = Engine.GetUserDetails(username, out var userDetails);

            //    rc = Engine.AllowUser(userDetails.UserId, out var denial);

            //    rc = Engine.AllowUser(userDetails.UserId, out denial); 

            //    Assert.AreNotEqual(ReturnCode.Success, rc);
            //    Assert.AreEqual(0,denial);
        }

        [Test]
        public void DenyUserWhoIsAllowed_ReturnsSuccess()
        {
            //    User user = new User()
            //    {
            //        Username = Util.GenerateRandomString(28),
            //        CreatedOn = DateTime.UtcNow,
            //    };


            //    var rc = Engine.RegisterUser(user, out user);
            //    MasterAllow allowal = new MasterAllow()
            //    {
            //        UserId = user.UserId, 
            //    };
            //    rc = Engine.AllowUser(allowal, out var allow);


            //    MasterDeny denial = new MasterDeny()
            //    {
            //        UserId = user.UserId,
            //        BannedAt = DateTime.UtcNow,
            //        ExpiresAt = DateTime.MaxValue,
            //        Reason = "Test Ban"
            //    };
            //    rc = Engine.DenyUser(denial, out denial);

            //    rc = Engine.IsUserAllowed(user, out bool x);


            //    Assert.AreEqual(ReturnCode.Success, rc);
            //    Assert.IsFalse(x);
            //    Assert.IsNotNull(denial);
        }
    }
}
