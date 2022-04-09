
using Proline.ResourceFramework.Exporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.ResourceFramework.Logging;
using Proline.ResourceFramework;

namespace Proline.Example.Controllers
{
    [ExportContainer]
    public class ExampleExports
    {
        protected Log _log = Logger.GetInstance().GetLog();

        [Export]
        public void Test()
        {
            _log.Debug("Called");
        }
    }
}
