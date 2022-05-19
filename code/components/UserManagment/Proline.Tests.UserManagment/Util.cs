using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit
{
    public static class Util
    {
        public static string GenerateRandomString(int length)
        {
            return Guid.NewGuid().ToString();
            //var random = new Random();
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //return new string(Enumerable.Repeat(chars, length)
            //  .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
