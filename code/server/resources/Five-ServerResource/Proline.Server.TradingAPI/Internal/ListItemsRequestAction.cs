using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Pro.Trading.Client;
using System.Text;
using System.Threading.Tasks;

namespace CTrading
{
    public class ListItemsRequestAction : ExtendedEvent
    {
        private ListItemsRequestAction _event;

        public ListItemsRequestAction() : base("ListItemsRequestHandler", true)
        {
        }

        public void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new ListItemsRequestAction();
                _event.Subscribe();
            }
        }

        public void UnsubscribeEvent()
        {
            if (_event != null)
            {
                _event.Unsubscribe();
                _event = null;
            }
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            var client = new HttpClient();
            var api = new TradingAPI("http://pro-trading-webapi", client);
            var x = api.ListItemsAsync();
            return JsonConvert.SerializeObject(x.Result.Items);
            //return null;
        }
    }
}
