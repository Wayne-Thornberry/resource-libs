using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Freemode.Client
{
    public static class FreemodeAPI
    {
        public static bool IsEntityInActivationRange(int entHandle)
        {
            var args = new object[1] { entHandle };
            return (bool)APICaller.CallAPI(-1536245805, args);
        }
    }
}
