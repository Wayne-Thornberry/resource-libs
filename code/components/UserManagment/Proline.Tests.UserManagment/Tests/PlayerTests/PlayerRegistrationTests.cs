﻿using NUnit.Framework;
using Proline.DBAccess.Data;
using Proline.DBAccess.MidLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Tests.PlayerTests
{
    public class PlayerRegistrationTests
    {
        //private AuthenticationHeaderValue _authHeader;

        [SetUp]
        public void Setup()
        {
            //_authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        }

        [Test]
        public void BasicPlayerRegistrationSuccess()
        {

            var request = new RegisterPlayerRequest();
            request.Name = Guid.NewGuid().ToString();
            RegisterPlayerResponse response = null;
            using (var api = new DBAccessApi())
            {
                response = api.RegisterPlayer(request);
            }

            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ReturnCode);
            Assert.Greater(response.Id, 0);
        }

        [Test]
        public void BasicPlayerAlreadyRegisteredSystemError()
        {

            var request = new RegisterPlayerRequest();
            request.Name = Guid.NewGuid().ToString();
            var username = request.Name;
            RegisterPlayerResponse response = null;
            using (var api = new DBAccessApi())
            {
                response = api.RegisterPlayer(request);
            }

            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ReturnCode);
            Assert.Greater(response.Id, 0);

            request = new RegisterPlayerRequest();
            request.Name = username;
            response = null;
            using (var api = new DBAccessApi())
            {
                response = api.RegisterPlayer(request);
            }

            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.ReturnCode); 


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

        [Test]
        public void RegisterPlayerExistingUserSuccess()
        {

            //RegistrationPlayerOutParameter outParameter = PlayerHelper.RegisterNewPlayer(out var identities);
            //RegistrationPlayerOutParameter outParameter2 = null;//PlayerHelper.RegisterNewPlayer(out var identities);
            //var username = Util.GenerateRandomString(15);
            //var identity1 = IdentityHelper.GenerateIdentifier(0); //identities.First().Identifier;
            //var identity2 = identities[1].Identifier;
            //var identity3 = identities[2].Identifier;
            //var identity4 = identities[3].Identifier;

            //UserAccountOutParameter user = null;

            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //    var userCleint = new Client("http://localhost:9703/", httpClient);
            //    outParameter2 = userCleint.RegisterPlayerAsync(new RegistrationPlayerInParameter()
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
            //Assert.NotNull(outParameter2);
            //Assert.AreNotEqual(outParameter.PlayerId, outParameter2.PlayerId);
            //Assert.AreEqual(outParameter.UserId, outParameter2.UserId);
            //Assert.AreEqual(user.Players.Count, 2);
        }

        [Test]
        public void RegisterPlayerExistingIdentitySuccess()
        {
            //var username = Util.GenerateRandomString(15);
            //RegistrationPlayerOutParameter outParameter = PlayerHelper.RegisterNewPlayer(out var identities);
            //var identity1 = identities[0].Identifier;
            //var identity2 = IdentityHelper.GenerateIdentifier(1);
            //var identity3 = IdentityHelper.GenerateIdentifier(2);
            //var identity4 = IdentityHelper.GenerateIdentifier(3);
            //RegistrationPlayerOutParameter outParameter2 = null;

            //using (var httpClient = new HttpClient())
            //{
            //    var _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
            //    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            //    var userCleint = new Client("http://localhost:9703/", httpClient);
            //    outParameter2 = userCleint.RegisterPlayerAsync(new RegistrationPlayerInParameter()
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

            //}

            //Assert.IsNotNull(outParameter2);
            //Assert.AreEqual(outParameter2.PlayerId, 0);
            //Assert.AreEqual(outParameter2.UserId, 0);
            //Assert.IsNull(outParameter2.Username);
        }

        [Test]
        public void RegisterPlayerNewIdentity_ReturnsSuccess()
        {

            //var identifiers = IdentityHelper.CreatePlayerIdentifiers(
            //    new[] { IdentityHelper.GenerateIdentifier(0), IdentityHelper.GenerateIdentifier(1), IdentityHelper.GenerateIdentifier(2), IdentityHelper.GenerateIdentifier(3) },
            //    new[] { 0, 1, 2, 3 });

            //var rc = Engine.RegisterPlayer(Util.GenerateRandomString(25), identifiers);

            //Assert.AreEqual(ReturnCode.Success, rc);
        }

        [Test]
        public void RegisterPlayerExistingIdentity_ReturnsFailure()
        {
            //var identifiers = IdentityHelper.CreatePlayerIdentifiers(
            //    new[] { "X", "Y", "Z", "A" },
            //    new[] { 0, 1, 2, 3 });

            //var rc = Engine.RegisterPlayer("ReservedUser", identifiers);

            //Assert.AreNotEqual(ReturnCode.Success, rc);
        }

        [Test]
        public void RegisterPlayerExistingIdentity_ReturnsSuccess()
        {
            //var identifiers = IdentityHelper.CreatePlayerIdentifiers(
            //    new[] { IdentityHelper.GenerateIdentifier((int)IdentifierType.SOCIAL), "Y", "Z", "A" },
            //    new[] { 0, 1, 2, 3 });

            //var rc = Engine.RegisterPlayer("ReservedUser", identifiers);


            //Assert.AreEqual(ReturnCode.Success, rc);
        }

    }
}
