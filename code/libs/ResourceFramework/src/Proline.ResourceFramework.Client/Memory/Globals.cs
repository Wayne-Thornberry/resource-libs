using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class Globals
    {
        private static StateBag _stateBag;

        internal static void SetGlobalBag(StateBag exports)
        {
            _stateBag = exports;
        }

        private static StateBag GetExports()
        {
            return _stateBag;
        }

        public static void AddGlobal(string key, object deleget)
        {
            _stateBag.Set(key, deleget, true);
        }

        public static object GetGlobal(string key)
        {
            return _stateBag.Get(key);
        }
    }
}
