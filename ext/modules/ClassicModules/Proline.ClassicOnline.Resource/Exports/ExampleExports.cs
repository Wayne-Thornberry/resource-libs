
using Proline.Resource.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Proline.ClassicOnline.Server.Exports
{
   // [ExportContainer]
    public class ExampleExports
    {
        protected Log _log => new Log();

      //  [Export]
        public void Test()
        {
            _log.Debug("Called");
        }
    }
}
