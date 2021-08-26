using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class Persistence
    {
        public static object Get(string key)
        {
            var access = EngineService.GetInstance();
            return access.GetGlobal(key);
        }

        public static void Set(string key, object data)
        { 
            var access = EngineService.GetInstance();
            access.SetGlobal(key, data, true);
        }
    }
}
