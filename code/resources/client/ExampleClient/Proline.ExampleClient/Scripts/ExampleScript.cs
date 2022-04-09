using Proline.ResourceFramework; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Scripts
{
    public class ExampleScript : ResourceScript
    {
        private Log _log = new Log();

        public override async Task OnStart()
        {
            _log.Debug("Start");
        }
        public override async Task OnUpdate()
        {

        }
    }
}
