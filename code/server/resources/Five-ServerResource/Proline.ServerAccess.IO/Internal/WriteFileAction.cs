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
    public class WriteFileAction : ExtendedEvent
    {
        private static WriteFileAction _event;

        public WriteFileAction() : base(EventNameDefinitions.SaveFileHandler, false)
        {

        }


        public void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new WriteFileAction();
                _event.Subscribe();
            }
        }

        protected override object OnEventTriggered(Player player, params object[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    var argPath = args[0].ToString();
                    var data = args[1].ToString();
                    var LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var DEFAULT_PATH = Path.Combine(LocalPath, "ProjectOnline");
                    var newPath = Path.Combine(DEFAULT_PATH, argPath);

                    string fileAndPath = newPath;
                    string currentDirectory = Path.GetDirectoryName(fileAndPath);
                    string fullPathOnly = Path.GetFullPath(currentDirectory);

                    if (!Directory.Exists(fullPathOnly))
                        Directory.CreateDirectory(fullPathOnly);
                    File.WriteAllText(newPath, data);
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
