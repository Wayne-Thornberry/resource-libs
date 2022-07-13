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
    public class WriteFileAction : ExtendedEvent
    {
        private static WriteFileAction _event;
         
        internal WriteFileAction() : base(SAVEFILEHANDLER, false)
        {

        }


        public const string SAVEFILEHANDLER = "SaveFileHandler";
        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new WriteFileAction();
                _event.Subscribe();
            }
        }
#if CLIENT
#elif SERVER

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            if (args.Length > 0)
            { 
                var argPath = args[0].ToString();
                var data = args[1].ToString();
                var LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var DEFAULT_PATH = Path.Combine(LocalPath, "ProjectOnline");
                File.WriteAllText(Path.Combine(DEFAULT_PATH, argPath), data);
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
