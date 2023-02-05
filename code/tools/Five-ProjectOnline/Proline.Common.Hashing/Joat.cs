using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Common.Hashing
{
    public static class Joat
    {
        public static long GetHash(string key)
        {
            var hash = 0;
            int x = key.Length;
            var chars = key.ToCharArray();

            for (int i = x - 1; i >= 0; i--)
            {
                hash += chars[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }
    }
}
