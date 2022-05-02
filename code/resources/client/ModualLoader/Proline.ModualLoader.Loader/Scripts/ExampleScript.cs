using System.Threading.Tasks;
using Proline.Resource.Framework;
using Proline.Resource.Logging;

namespace Proline.ResourceLoader.Main.Scripts
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
