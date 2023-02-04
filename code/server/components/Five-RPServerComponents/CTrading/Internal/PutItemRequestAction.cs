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
    public class PutItemRequestAction : ExtendedEvent
    {
        private PutItemRequestAction _event;

        public PutItemRequestAction() : base("PutItemRequestHandler", false)
        {
        }

        public void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PutItemRequestAction();
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
            var x = api.InsertItemAsync(new InsertItemRequest()
            {
                Item = new Item()
                {
                    Name = args[0].ToString(),
                    Price = new Random().Next()
                }
            });
            return null;
        }
    }
}
