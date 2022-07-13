using Proline.ServerCore.IO.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerCore.IO
{
    internal static class ServerFile
    {
#if CLIENT
        public static async Task<string> ReadLocalFile(string path)
        {
            var x = new ReadFileAction();
            x.Invoke(path);
            await x.WaitForCallback();
            return x.Data;
        }

        public static async Task<string> WriteLocalFile(string path)
        {
            var x = new ReadFileAction();
            x.Invoke(path);
            await x.WaitForCallback();
            return x.Data;
        }
#endif
    }
}
