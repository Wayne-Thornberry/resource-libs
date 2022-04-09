using Proline.ResourceFramework.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Controllers
{
    [ExportController]
    public class ExampleExports
    {
        [ControllerMethod("Test", "Export")]
        public void Test()
        {

        }
    }
}
