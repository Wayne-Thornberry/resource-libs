using Proline.ServerAccess.IO.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerAccess.IO
{
    public static class ServerConsole
    {
         
        public static async Task<string> WriteLine(string data)
        {
            var x = new ConsoleWriteLineAction();
            x.Invoke(data);
            await x.WaitForCallback();
            return null;
        }

        public static async Task<string> Write(string data)
        {
            var x = new ConsoleWriteAction();
            x.Invoke(data);
            await x.WaitForCallback();
            return null;
        } 
    }
}
