using Microsoft.VisualBasic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Proline.DBAccess.NUnit.Tests.UsersTests
{
    [TestFixture]
    public class CreateUserTests
    {
        private AuthenticationHeaderValue _authHeader;

        [SetUp]
        public void Setup()
        {
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        }


        [Test]
        public void RetriveUserBadRequest()
        {
            //    UserAccountOutParameter account = null;

            //    using (var httpClient = new HttpClient())
            //    {
            //        Assert.Catch(typeof(BadRequestException), () =>
            //        {
            //            httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //            var userCleint = new Client("http://localhost:9703/", httpClient);
            //            account = userCleint.UserGETAsync(-1).Result;
            //        });
            //    }
            //    Assert.Null(account);
            //    //Assert.AreEqual(account.Username, username);
        }


        [Test]
        public void RetriveUserInternalProblem()
        {
            //    UserAccountOutParameter account = null;

            //    using (var httpClient = new HttpClient())
            //    {
            //        Assert.Catch(typeof(Exception), () =>
            //        {
            //            httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //            var userCleint = new Client("http://localhost:9703/", httpClient);
            //            account = userCleint.UserGETAsync(0).Result;
            //        });
            //    }
            //    Assert.Null(account);
            //    //Assert.AreEqual(account.Username, username);
        }

        [Test]
        public void InsertUserSuccess()
        {
            //    var username = Util.GenerateRandomString(15);
            //    var inParams = new UserAccountInParameter()
            //    {
            //        Username = username,
            //        Identifiers = new List<IdentifierInParameter>()
            //            {
            //                new IdentifierInParameter()
            //                {
            //                    Identifier = IdentityHelper.GenerateIdentifier(0),
            //                    IdentitierType = 0,
            //                },
            //                new IdentifierInParameter()
            //                {
            //                    Identifier = IdentityHelper.GenerateIdentifier(1),
            //                    IdentitierType = 1,
            //                },
            //                new IdentifierInParameter()
            //                {
            //                    Identifier = IdentityHelper.GenerateIdentifier(2),
            //                    IdentitierType = 2,
            //                },
            //                new IdentifierInParameter()
            //                {
            //                    Identifier = IdentityHelper.GenerateIdentifier(3),
            //                    IdentitierType = 3,
            //                },

            //            }
            //    };


            //    using (var httpClient = new HttpClient())
            //    {
            //        Assert.DoesNotThrow(() =>
            //        {
            //            httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //            var userCleint = new Client("http://localhost:9703/", httpClient);
            //            userCleint.UserPOSTAsync(inParams).Wait();
            //        });
            //    }
        }

        [Test]
        public void InsertUserVerifyIdentity()
        {
            //    UserHelper.InsertRandomUser(out var username, out var identities);
            //    IdentityOutParameter userIdentity = null;
            //    using (var httpClient = new HttpClient())
            //    {
            //        httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //        var userCleint = new Client("http://localhost:9703/", httpClient);
            //        userIdentity = userCleint.IdentityGETAsync(identities[0].Identifier).Result;
            //    }
            //    Assert.NotNull(userIdentity);
            //    Assert.AreEqual(userIdentity.Identifier, identities[0].Identifier);
        }

        [Test]
        public void InsertUserVerifyInsertedUser()
        {
            //    UserHelper.InsertRandomUser(out var username, out var identities);
            //    UserAccountOutParameter account = null;
            //    using (var httpClient = new HttpClient())
            //    {
            //        httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //        var userCleint = new Client("http://localhost:9703/", httpClient);
            //        var userIdentity = userCleint.IdentityGETAsync(identities[0].Identifier).Result;
            //        account = userCleint.UserGETAsync(userIdentity.UserId).Result;
            //    }
            //    Assert.NotNull(account);
            //    Assert.AreEqual(account.Username, username);
        }

        [Test]
        public void InsertUserDenyUserVerifyDenial()
        {
            //    // Arrange
            //    UserHelper.InsertRandomUser(out var username, out var identities);
            //    UserAccountOutParameter account = null;
            //    UserDenyOutParameter deny = null;

            //    UserDenyInParameter den = new UserDenyInParameter()
            //    {
            //        UserId = -1,
            //        Reason = "Test Ban",
            //        Untill = DateTime.UtcNow,
            //    };

            //    // Act
            //    using (var httpClient = new HttpClient())
            //    {
            //        httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //        var userCleint = new Client("http://localhost:9703/", httpClient);
            //        var userIdentity = userCleint.IdentityGETAsync(identities[0].Identifier).Result;
            //        account = userCleint.UserGETAsync(userIdentity.UserId).Result;
            //        den.UserId = account.UserId;
            //        userCleint.UserDenyPOSTAsync(den).Wait(); 
            //        deny = userCleint.UserDenyGETAsync(account.UserId).Result;
            //    }

            //    // Assert
            //    Assert.NotNull(deny);
            //    Assert.AreEqual(den.Reason, deny.Reason);
        }


    }
}
