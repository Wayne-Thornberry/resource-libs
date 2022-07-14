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
    public class ConsoleWriteLineAction : ExtendedEvent
    {
        private static ConsoleWriteLineAction _event;
         
        internal ConsoleWriteLineAction() : base(EVENTHANDLER, false)
        {

        }


        public const string EVENTHANDLER = "ConsoleWriteLineHandler";
        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new ConsoleWriteLineAction();
                _event.Subscribe();
            }
        }
#if CLIENT
#elif SERVER

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            try
            {

                if (args.Length > 0)
                {
                    var data = args[0].ToString();
                    Console.WriteLine(data);
                    return null;
                }
                else
                {
                    Console.WriteLine("Argument count does not match expected count");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
#endif
    }
}
