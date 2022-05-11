using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerLauncher.Program
{
    public class ManifestData
    {
        public string[] FilePaths { get; set; }
    }

    public class FileLoader
    {
        private string _dir;
        public FileLoader(string dir)
        {
            _dir = dir;
        }

        public string[] Load(ManifestData manifest)
        {
            var list = new List<string>();
            foreach (var filePath in manifest.FilePaths)
            {
                list.Add(Load(filePath));
            }
            return list.ToArray();
        }

        public string Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(Path.Combine(_dir, filePath));
                return text;
            }
            return null;
        }
    }
}
