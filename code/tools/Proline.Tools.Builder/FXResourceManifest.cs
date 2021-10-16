using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Personal.Loader
{
    public class FXResourceManifest
    {
        public string ManifestVersion { get; set; }
        public string FXVersion { get; set; }
        public string[] Games { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string ClientScript { get; set; }
        public string ServerScript { get; set; }
        public string[] Files { get; set; }

        public override string ToString()
        {


            var mfVersion = "";
            var fxVersion = "";
            var games = "";
            var author = "";
            var description = "";
            var version = "";
            var clientScript = "";
            var serverScript = "";
            var files = "";

            if(!string.IsNullOrEmpty(ManifestVersion))
                mfVersion = $"fx_version '{ManifestVersion}'"; 
            if (!string.IsNullOrEmpty(FXVersion))
                fxVersion = $"resource_manifest_version '{FXVersion}'";
            if (Games != null && Games.Length > 0)
            {
                var gameStr = "";
                foreach (var game in Games)
                {
                    gameStr += $" '{game}' ";
                }
                games = "games {" + $"{gameStr}" + "}";
            }
            if (!string.IsNullOrEmpty(Author))
                author = $"author '{Author}'";
            if (!string.IsNullOrEmpty(Description))
                description = $"description '{Description}'";
            if (!string.IsNullOrEmpty(Version))
                version = $"version '{Version}'";
            if (!string.IsNullOrEmpty(ClientScript))
                clientScript = $"client_script '{ClientScript}'";
            if (!string.IsNullOrEmpty(ServerScript))
                serverScript = $"server_script '{ServerScript}'";

            if (Files != null && Files.Length > 0)
            {
                var fileStr = "";
                foreach (var item in Files)
                {
                    fileStr += "\n'" + item + "',";
                }
                files = "files {" + fileStr + "\n}";
            }

            return string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}", fxVersion, mfVersion, games, author, description, version, clientScript, serverScript, files);
        }
    }
}
