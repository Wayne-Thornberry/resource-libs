using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.NUnit.Tests.UserTests
{
    [TestFixture]
    public class DenyUserTests
    {
        //[Test]
        //public void DenyUser_ReturnsSuccess()
        //{

        //    var username = Util.GenerateRandomString(28);
        //    var rc = Engine.RegisterUser(username);
        //    Assert.AreEqual(ReturnCode.Success, rc);

        //    rc = Engine.GetUserDetails(username, out var userDetails);

        //    rc = Engine.DenyUser(userDetails.UserId, out var denial);


        //    Assert.AreEqual(ReturnCode.Success, rc);
        //    Assert.IsNotNull(denial);
        //}

        //[Test]
        //public void DenyUserTwice_ReturnsFailure()
        //{

        //    var username = Util.GenerateRandomString(28);
        //    var rc = Engine.RegisterUser(username);
        //    Assert.AreEqual(ReturnCode.Success, rc);

        //    rc = Engine.GetUserDetails(username, out var userDetails);

        //    rc = Engine.DenyUser(userDetails.UserId, out var denial);

        //    rc = Engine.DenyUser(userDetails.UserId, out denial);


        //    Assert.AreNotEqual(ReturnCode.Success, rc);
        //    Assert.AreEqual(0,denial);
        //}

        //[Test]
        //public void DenyUserWhoIsAllowed_ReturnsSuccess()
        //{ 
             
        //    var username = Util.GenerateRandomString(28);
        //    var rc = Engine.RegisterUser(username);
        //    Assert.AreEqual(ReturnCode.Success, rc);

        //    rc = Engine.GetUserDetails(username, out var userDetails);

        //    rc = Engine.AllowUser(userDetails.UserId, out var allow); 

        //    rc = Engine.DenyUser(userDetails.UserId, out var denial);

        //    rc = Engine.IsUserAllowed(userDetails.UserId, out bool x);


        //    Assert.AreEqual(ReturnCode.Success, rc);
        //    Assert.IsFalse(x);
        //    Assert.IsNotNull(denial);
        //}
    }
}
