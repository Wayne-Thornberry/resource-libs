
using Proline.ServerAccess.IO.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerAccess.IO
{
    public static class ServerFile
    { 
        public static async Task<string> ReadLocalFile(string path)
        {
            var x = new ReadFileAction();
            x.Invoke(path);
            await x.WaitForCallback();
            return x.Data;
        }

        public static async Task<string> WriteLocalFile(string path, string data)
        {
            var x = new WriteFileAction();
            x.Invoke(path, data);
            await x.WaitForCallback();
            return null;
        } 
    }
}
