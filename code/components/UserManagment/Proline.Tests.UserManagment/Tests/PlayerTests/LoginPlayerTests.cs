using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Tests.PlayerTests
{
    public class LoginPlayerTests
    {
        private AuthenticationHeaderValue _authHeader;

        [SetUp]
        public void Setup()
        {
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        }

        [Test]
        public void LoginPlayerBasicSuccess()
        {
            //RegistrationPlayerOutParameter outParameter = PlayerHelper.RegisterNewPlayer(out var identities);
            //LoginPlayerOutParameter result = null;

            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //    var userCleint = new Client("http://localhost:9703/", httpClient);
            //    result = userCleint.LoginPlayerAsync(new LoginPlayerInParameter() { Identifier = identities[0].Identifier }).Result;
            //}

            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.PlayerId, outParameter.PlayerId);
            //Assert.AreEqual(result.UserId, outParameter.UserId);
        }

        [Test]
        public void LoginPlayerNoPlayerFoundSuccess()
        {
            // RegistrationPlayerOutParameter outParameter = PlayerHelper.RegisterNewPlayer(out var identities);
            //var identity = IdentityHelper.GenerateIdentifier(0);
            //LoginPlayerOutParameter result = null;

            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //    var userCleint = new Client("http://localhost:9703/", httpClient);
            //    result = userCleint.LoginPlayerAsync(new LoginPlayerInParameter() { Identifier = identity }).Result;
            //}

            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.PlayerId, 0);
            //Assert.AreEqual(result.UserId, 0);
        }
    }
}
