using NUnit.Framework;
using Proline.Online.Data;
using Proline.CentralEngine.MidLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.NUnit.Helpers
{
    public static class UserHelper
    {
        public static void SetupUser(out long userId)
        {
            var identifiers = IdentityHelper.CreatePlayerIdentifiers(
          new[] { IdentityHelper.GenerateIdentifier(0), IdentityHelper.GenerateIdentifier(1), IdentityHelper.GenerateIdentifier(2), IdentityHelper.GenerateIdentifier(3) },
          new[] { 0, 1, 2, 3 });

            var username = Util.GenerateRandomString(28);
            var rc = Engine.RegisterUser(username);
            Assert.AreEqual(ReturnCode.Success, rc);

            rc = Engine.GetUserDetails(username, out var userDetails);
            Assert.AreEqual(ReturnCode.Success, rc);

            userId = userDetails.UserId;
        }
    }
}
