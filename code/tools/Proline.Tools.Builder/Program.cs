using Project.Personal.Loader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Project.Tools.Builder
{

    public class Program
    {
        public Program()
        {

        }

        public static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
            Console.ReadKey();
        }
        public static string GetAssemblyFileVersion()
        { 
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Run()
        {
            // Pull latest build from git/svn
            Console.WriteLine("FXManifest Generated");
            GenerateFXManifest();
        }

        private void LaunchClientInstance()
        {
            Process.Start("http://google.com");
        }

        private void GenerateFXManifest()
        {
            var resourceDir = Path.Combine(Directory.GetCurrentDirectory());
            var manifestDir = Path.Combine(resourceDir, "fxmanifest.lua"); 

            var filePaths = new List<string>();
            filePaths.AddRange(GetFilePathsInDir(resourceDir));

            string author = ConfigurationManager.AppSettings["Author"];
            string mVersion = ConfigurationManager.AppSettings["ManifestVersion"];
            string fxVersion = ConfigurationManager.AppSettings["FXVersion"];
            string descritpion = ConfigurationManager.AppSettings["Description"];
            string script = ConfigurationManager.AppSettings["Script"];

            FXResourceManifest fxManifest = null;
            if(Regex.Match(resourceDir,@"Client").Success)
            { 
                fxManifest = new FXBuilder()
                    .BuildManifestVersion(fxVersion)
                    .BuildFXVersion(mVersion)
                    .BuildGame(new[] { "gta5" })
                    .BuildAuthorName(author)
                    .BuildDescription(descritpion)
                    .BuildVersion(GetAssemblyFileVersion())
                    .BuildClientScript(script) 
                    .BuildFiles(filePaths.ToArray())
                    .Build();
            }
            else
            {
                fxManifest = new FXBuilder()
                    .BuildManifestVersion(fxVersion)
                    .BuildFXVersion(mVersion)
                    .BuildGame(new[] { "gta5" })
                    .BuildAuthorName(author)
                    .BuildDescription(descritpion)
                    .BuildVersion(GetAssemblyFileVersion()) 
                    .BuildServerScript(script)
                    .BuildFiles(filePaths.ToArray())
                    .Build();
            }
            File.WriteAllText(manifestDir, fxManifest.ToString());
        }

        private IEnumerable<string> GetFilePathsInDir(string dataDir)
        {
            if (Directory.Exists(dataDir))
            {
                return DirSearch(dataDir);
            }
            return new string[0];
        }

        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            { 
                string regex = ConfigurationManager.AppSettings["Regex"];
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (Regex.Match(f, @"(fxmanifest)|(.net.*)|(.pdb)|(.config)|(.exe)").Success) continue;
                    Console.WriteLine(Regex.Replace(f, regex, "'").ToString());
                    files.Add(Regex.Replace(f,regex, "").Replace(@"\", @"\\").ToString());
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                throw excpt;
            }

            return files;
        }
    }
}
