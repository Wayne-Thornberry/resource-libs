using Proline.ResourceFramework.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Controllers
{
    [EventController]
    public class ExampleController
    {
        [EventAPI]
        public void TestAPI()
        {

        }
    }
}
