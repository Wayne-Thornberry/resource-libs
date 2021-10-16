using Project.Personal.Loader;
using System;

namespace Project.Tools.Builder
{
    internal class FXBuilder
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

        public FXBuilder()
        {
        }

        internal FXBuilder BuildManifestVersion(string v)
        {
            ManifestVersion = v;
            return this;
        }

        internal FXBuilder BuildFXVersion(string v)
        {
            FXVersion = v;
            return this;
        }

        internal FXBuilder BuildGame(string[] vs)
        {
            Games = vs;
            return this;
        }

        internal FXBuilder BuildAuthorName(string v)
        {
            Author = v;
            return this;
        }

        internal FXBuilder BuildDescription(string v)
        {
            Description = v;
            return this;
        }

        internal FXBuilder BuildVersion(string v)
        {
            Version = v;
            return this;

        }

        internal FXBuilder BuildClientScript(string v)
        {
            ClientScript = v;
            return this;
        }

        internal FXBuilder BuildServerScript(string v)
        {
            ServerScript = v;
            return this;
        }

        internal FXBuilder BuildFiles(string[] filePaths)
        {
            Files = filePaths;
            return this;
        }

        internal FXResourceManifest Build()
        {
            return new FXResourceManifest()
            {
                Author = Author,
                Files = Files,
                ClientScript = ClientScript,
                Description = Description,
                FXVersion = FXVersion,
                Games = Games,
                ManifestVersion = ManifestVersion,
                ServerScript = ServerScript,
                Version = Version,
            };
        }
    }
}