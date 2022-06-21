using NUnit.Framework;
using Proline.DBAccess.Data;
using Proline.DBAccess.MidLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proline.DBAccess.NUnit.Tests.PlayerTests
{
    [TestFixture]
    public class GetPlayerTests
    {

        [Test]
        public void BasicGetPlayerSuccess()
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

            var request2 = new GetPlayerRequest();
            request2.Username = request.Name;
            GetPlayerResponse response2 = null;
            using (var api = new DBAccessApi())
            {
                response2 = api.GetPlayer(request2);
            }


            Assert.IsNotNull(response2);
            Assert.AreEqual(0, response2.ReturnCode);
            Assert.Greater(response2.PlayerId, 0);


        }
    }
}
