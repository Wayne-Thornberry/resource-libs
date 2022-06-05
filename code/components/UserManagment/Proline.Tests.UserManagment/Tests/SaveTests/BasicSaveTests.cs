using NUnit.Framework;
using Proline.DBAccess.Data;
using Proline.DBAccess.MidLayer; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Tests.SaveTests
{
    [TestFixture]
    internal class BasicSaveTests
    {
        [Test]
        public void BasicSaveFileSuccess()
        {
            InsertSaveResponse response = null;
            var request = new InsertSaveRequest();
            request.PlayerId = 1;
            request.Data = "{}";

            using (var api = new DBAccessApi())
            {
                response = api.SaveFile(request);
            }

            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ReturnCode);
            Assert.Greater(response.Id, 0);
        }

        [Test]
        public void UpdateSaveFileSuccess()
        {
            InsertSaveResponse response = null;
            InsertSaveRequest request = new InsertSaveRequest();

            InsertSaveResponse response2 = null;
            InsertSaveRequest request2 = new InsertSaveRequest();

            GetSaveRequest request3 = new GetSaveRequest();
            GetSaveResponse response3 = null;

            request.PlayerId = 1;
            request.Data = "{}";

            request2 = new InsertSaveRequest();
            request2.PlayerId = 1;
            request2.Data = "{ 'TestData' : 'Wow' }";


            using (var api = new DBAccessApi())
            {
                response = api.SaveFile(request);
                response2 = api.SaveFile(request2);
                request3.Id = response2.Id;
                response3 = api.GetSave(request3);
            }



            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ReturnCode);
            Assert.Greater(response.Id, 0);

            Assert.IsNotNull(response2);
            Assert.AreEqual(0, response2.ReturnCode);
            Assert.Greater(response2.Id, 0);

            Assert.IsNotNull(response3);
            Assert.AreEqual(0, response3.ReturnCode);
            Assert.AreEqual(request2.Data, response3.Data);
        }
    }
}
