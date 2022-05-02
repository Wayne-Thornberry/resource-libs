
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class HostingAbstractionsHostExtensions
    {
        public static void Run(this IHost host)
        {
            host.Start();
        }
    }
}
