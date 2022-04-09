using CitizenFX.Core;
using Proline.ResourceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.APIs
{
    public class DebugAPI : IDebugMethods
    { 
        public void Write(object obj)
        {
            Debug.Write(obj.ToString());
        }

        public void WriteLine(object obj)
        {
            Debug.WriteLine(obj.ToString());
        }
    }
}
