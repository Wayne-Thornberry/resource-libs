using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ServerAccess.IO.Actions
{
    public class ConsoleWriteAction : ExtendedEvent
    {
        private static ConsoleWriteAction _event;
         
        internal ConsoleWriteAction() : base(EVENTHANDLER, false)
        {

        }


        public const string EVENTHANDLER = "ConsoleWriteHandler";
        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new ConsoleWriteAction();
                _event.Subscribe();
            }
        }
#if CLIENT
#elif SERVER

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            if (args.Length > 0)
            { 
                var data = args[0].ToString();
                Console.Write(data);
                return null;
            }
            else
            {
                Console.WriteLine("Argument count does not match expected count");
                return null;
            }
        }
#endif
    }
}
