using CitizenFX.Core;
using Pro.Trading.Client;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CTrading
{
    public class DeleteItemRequestAction : ExtendedEvent
    {
        public DeleteItemRequestAction() : base("DeleteItemRequestHandler", true)
        {
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            var client = new HttpClient();
            var api = new TradingAPI("http://pro-trading-webapi", client);
            var x = api.DeleteItemAsync(new DeleteItemRequest()
            {
                Name = args[0].ToString(),
            });
            return null;
        }
    }
}
