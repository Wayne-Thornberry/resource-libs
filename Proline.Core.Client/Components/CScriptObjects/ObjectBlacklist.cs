using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CScriptObjects
{
    internal static class ObjectBlacklist
    {
        private static List<int> _blacklist;
        internal static void Add(int positionsPair)
        {
            _blacklist.Add(positionsPair);
        }

        internal static void Remove(int positionsPair)
        {
            _blacklist.Remove(positionsPair);
        }

        internal static bool Contains(int positionsPair)
        {
           return _blacklist.Contains(positionsPair);
        }

        internal static IEnumerable<int> GetList()
        {
            return _blacklist.ToArray();
        }
        internal static void Create()
        {
            _blacklist = new List<int>();
        }
    }
}
