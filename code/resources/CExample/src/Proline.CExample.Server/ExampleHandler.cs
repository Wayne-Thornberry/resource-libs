using Proline.Resource.Component.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CExample
{
    public class ExampleHandler : ComponentHandler
    {
        [ComponentProperty]
        public int ExampleProperty { get; set; }
        public ExampleObject Example { get; set; }

        public ExampleHandler()
        {
        }

        [ComponentAPI]
        public void ExampleServerAPI()
        {
            LogDebug("Hey, it was called");
        }

        [ComponentAPI]
        public void ExampleServerAPI(long x, long y, long z)
        {
            LogDebug(string.Format("{0}{1}{2}",x,y,z));
        }

        [ComponentAPI]
        public long ExampleServerAPI2(long x, long y, long z)
        {
            return x + y + z;
        }
    }
}
