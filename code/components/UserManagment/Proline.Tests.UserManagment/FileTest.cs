using NUnit.Framework;
using Proline.DBAccess.Data;
using Proline.DBAccess.MidLayer;
using Proline.DBAccess.WebService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit
{
    [TestFixture]
    internal class FileTest
    {
        [Test]
        public void BasicSaveFileSuccess()
        {
            var request = new PlacePlayerDataRequest();

            using (var api = new DBAccessApi())
            {
                api.SaveFile(request);
            }
        }
    }
}
