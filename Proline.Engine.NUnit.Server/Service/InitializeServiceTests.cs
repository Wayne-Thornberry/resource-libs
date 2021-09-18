using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.NUnit
{
    [TestFixture]
    public class InitializeServiceTests
    {
        [Test]
        public void InitializeServiceNormally()
        {
             
            var service = new EngineService(new TestScript());
            service.Start(); 
        }
    }
}
