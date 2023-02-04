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

namespace CInputOutput
{
    public class ConsoleWriteAction : ExtendedEvent
    {
        private static ConsoleWriteAction _event;

        public ConsoleWriteAction() : base(EventNameDefinitions.ConsoleWriteHandler, false)
        {

        }

        public void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new ConsoleWriteAction();
                _event.Subscribe();
            }
        }
        protected override object OnEventTriggered(Player player, params object[] args)
        {
            try
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }
}
