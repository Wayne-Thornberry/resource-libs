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

namespace Proline.ServerCore.IO.Actions
{
    public class ReadFileAction : ExtendedEvent
    {
        private static ReadFileAction _event;

        private static string LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public string DEFAULT_PATH = Path.Combine(LocalPath, "ProjectOnline");

        public const string LOADFILEHANDLER = "LoadFileHandler";
        public string Data { get; set; }

        public ReadFileAction() : base(LOADFILEHANDLER, true)
        {

        }
        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new ReadFileAction();
                _event.Subscribe();
            }
        }

        public static void UnsubscribeEvent()
        {
            if (_event != null)
            {
                _event.Unsubscribe();
                _event = null;
            }
        }

#if CLIENT
        protected override void OnEventCallback(params object[] args)
        {
            Data = args[0].ToString();
        }
#elif SERVER

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            // old way via getting id
            if (args.Length > 0)
            {
                var argPath = args[0].ToString(); 
                var data = File.ReadAllText(argPath);  
                return data;
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
