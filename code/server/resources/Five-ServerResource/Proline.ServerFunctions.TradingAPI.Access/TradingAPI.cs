using Proline.ServerAccess.IO.Client.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerAccess.IO.Client
{
    public static class TradingAPI
    {
        public static async Task<string> ListItems()
        {
            var x = new ListItemsRequestAction();
            x.Invoke();
            await x.WaitForCallback();
            return x.Data;
        }

        public static void PutItem(string name)
        { 
            var x = new PutItemRequestAction();
            x.Invoke(name); 
        }
    }
}
