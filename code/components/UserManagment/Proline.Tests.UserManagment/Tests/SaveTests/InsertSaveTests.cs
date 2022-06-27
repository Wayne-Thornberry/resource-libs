using NUnit.Framework;
using Proline.DBAccess.Data;
using Proline.DBAccess.MidLayer;
using System;

namespace Proline.DBAccess.NUnit.Tests.SaveTests
{
    [TestFixture]
    internal class InsertSaveTests
    {
        [Test]
        public void BasicSaveFileSuccess()
        { 

            InsertSaveResponse response = null;
            var request = new InsertSaveRequest();
            request.Identity = Guid.NewGuid().ToString();
            request.PlayerId = 1;
            request.Data = "{}";


            using (var api = new DBAccessApi())
            {
                var res =  api.RegisterPlayer(new RegisterPlayerRequest() { Name = Guid.NewGuid().ToString() });
                request.PlayerId = res.Id;
                response = api.SaveFile(request);
            }

            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ReturnCode);
            Assert.IsNotNull(response.Identity);
        }

        [Test]
        public void BasicUpdateSaveFileSuccess()
        {
            InsertSaveResponse response = null;
            InsertSaveRequest request = new InsertSaveRequest();

            InsertSaveResponse response2 = null;
            InsertSaveRequest request2 = new InsertSaveRequest();

            GetSaveRequest request3 = new GetSaveRequest();
            GetSaveResponse response3 = null;

            request.Identity = Guid.NewGuid().ToString();
            request.PlayerId = 1;
            request.Data = "{}";

            request2 = new InsertSaveRequest();
            request2.Identity = request.Identity;
            request2.PlayerId = 1;
            request2.Data = "{ 'TestData' : 'Wow' }";


            using (var api = new DBAccessApi())
            {

                var res = api.RegisterPlayer(new RegisterPlayerRequest() { Name = Guid.NewGuid().ToString() });
                request.PlayerId = res.Id;
                request2.PlayerId = res.Id;

                response = api.SaveFile(request);
                response2 = api.SaveFile(request2);
                request3.Identity = response2.Identity;
                response3 = api.GetSave(request3);
            }



            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ReturnCode);
            Assert.IsNotNull(response.Identity);

            Assert.IsNotNull(response2);
            Assert.AreEqual(0, response2.ReturnCode);
            Assert.IsNotNull(response2.Identity);

            Assert.IsNotNull(response3);
            Assert.AreEqual(0, response3.ReturnCode);
            Assert.Greater(response3.SaveFiles.Length, 0);
            var saveFile = response3.SaveFiles[0];
            Assert.AreEqual(request2.Data, saveFile.Data);
        }
    }
}
