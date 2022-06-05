using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Tests.UsersTests
{
    public class DenyUserTests
    {
        private AuthenticationHeaderValue _authHeader;

        [SetUp]
        public void Setup()
        {
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        }

        [Test]
        public void CreateBasicUser()
        {
            //var username = Util.GenerateRandomString(15);
            //var identity1 = IdentityHelper.GenerateIdentifier(0);
            //var identity2 = IdentityHelper.GenerateIdentifier(1);
            //var identity3 = IdentityHelper.GenerateIdentifier(2);
            //var identity4 = IdentityHelper.GenerateIdentifier(3);
            //RegistrationPlayerOutParameter outParameter = null;

            //UserAccountOutParameter user = null;

            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //    var userCleint = new Client("http://localhost:9703/", httpClient);
            //    outParameter = userCleint.RegisterPlayerAsync(new RegistrationPlayerInParameter()
            //    {
            //        Username = username,
            //        Identifiers = new List<IdentifierCreateInParameter>() {
            //            new IdentifierCreateInParameter()
            //            {
            //                Identifier = identity1,
            //                IdentitierType = 0,
            //            },
            //            new IdentifierCreateInParameter()
            //            {
            //                Identifier = identity2,
            //                IdentitierType = 1,
            //            },
            //            new IdentifierCreateInParameter()
            //            {
            //                Identifier = identity3,
            //                IdentitierType = 2,
            //            },
            //            new IdentifierCreateInParameter()
            //            {
            //                Identifier = identity4,
            //                IdentitierType = 3,
            //            }

            //        }
            //    }).Result;

            //    user = userCleint.GetUserAsync(outParameter.UserId).Result;
            //}

            //Assert.NotNull(outParameter);
            //Assert.AreNotEqual(outParameter.PlayerId, 0);
            //Assert.AreEqual(outParameter.Username, username);
            //Assert.NotNull(user);
            //Assert.AreNotEqual(user.UserId, 0);
            //Assert.AreEqual(user.Players.Count, 1);
            //Assert.AreEqual(user.Identities.Count, 4);
        }
    }
}
