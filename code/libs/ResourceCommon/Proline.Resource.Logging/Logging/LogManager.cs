using Proline.ResourceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.Logging
{
    public class LogManager
    {
        internal static IDebugMethods Debug { get; private set; }

        internal LogManager()
        {

        }



        public static void Initialize(IDebugMethods debugAPI)
        {
            var lm = new LogManager();
            Debug = debugAPI;
        }
    }
}
