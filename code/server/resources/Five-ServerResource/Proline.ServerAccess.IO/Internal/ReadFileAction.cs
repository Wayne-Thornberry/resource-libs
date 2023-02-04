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
    public class ReadFileAction : ExtendedEvent
    {
        private static ReadFileAction _event;

        public string Data { get; set; }

        public ReadFileAction() : base(EventNameDefinitions.LoadFileHandler, true)
        {

        }
        public void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new ReadFileAction();
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
            try
            {
                // old way via getting id
                if (args.Length > 0)
                {
                    var argPath = args[0].ToString();
                    var LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var DEFAULT_PATH = Path.Combine(LocalPath, "ProjectOnline");
                    var newPath = Path.Combine(DEFAULT_PATH, argPath);

                    string fileAndPath = newPath;
                    string currentDirectory = Path.GetDirectoryName(fileAndPath);
                    string fullPathOnly = Path.GetFullPath(currentDirectory);

                    if (!Directory.Exists(fullPathOnly))
                        return null;

                    var data = File.ReadAllText(newPath);
                    return data;
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
