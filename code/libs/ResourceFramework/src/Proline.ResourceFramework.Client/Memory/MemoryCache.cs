using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class MemoryCache
    {
        private static Dictionary<string, object> _cache;

        public static void Cache(string name, object test)
        {
            if (_cache == null)
                _cache = new Dictionary<string, object>();
            _cache.Add(name, test);
        }

        public static object Retrive(string name)
        {
            if (_cache == null)
                _cache = new Dictionary<string, object>();
            return _cache[name];
        }
    }
}
