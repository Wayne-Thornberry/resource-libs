using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example
{
    public static class ExampleAPI
    {
        public static int Example()
        {
            //var args = new object[] { x, null };
            var result = APICaller.CallAPI<int>(1484629782, new object[0]);
            //y = (int)args[1];
            return result;
        }
    }
}
