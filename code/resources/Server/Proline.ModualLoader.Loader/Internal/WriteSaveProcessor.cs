using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace ProlineCore.Internal
{
    internal static class WriteSaveProcessor
    {
        private static string LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        internal static void WriteDataToLocal(string path, string data)
        {
            File.WriteAllText(path, data);
        }

        internal static void WriteSaveToLocal(Save save)
        {
            var player = save.Owner;
            var files = save.GetSaveFiles();
            foreach (var item in files)
            {
                var playerPath = Path.Combine(LocalPath, "ProjectOnline", "Saves", player.Identifiers["license"]);
                if (!Directory.Exists(playerPath))
                    Directory.CreateDirectory(playerPath);
                var path = Path.Combine(playerPath, string.Format("{0}.json", item.Identifier));
                Console.WriteLine(path);
                var json = JsonConvert.SerializeObject(item.Properties, Formatting.Indented);
                File.WriteAllText(path, json);
            }
        }

        internal static Save ReadSaveFromLocal(Player player)
        {
            var save = new Save(player);
            foreach (var item in player.Identifiers)
            {
                Console.WriteLine(item);
            }

            var playerPath = Path.Combine(LocalPath, "ProjectOnline", "Saves", player.Identifiers["license"]);
            Console.WriteLine(playerPath);
            if (Directory.Exists(playerPath))
            {
                foreach (var item in Directory.GetFiles(playerPath, "*.json"))
                {
                    var name = Path.GetFileName(item).Split('.')[0];
                    var json = File.ReadAllText(item);
                    Console.WriteLine(item);
                    var saveFile = new SaveFile
                    {
                        Properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(json),
                        Identifier = name
                    };
                    save.InsertSaveFile(saveFile);
                }
            }
            return save;
        }

        internal static string ReadDataFromLocal(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
