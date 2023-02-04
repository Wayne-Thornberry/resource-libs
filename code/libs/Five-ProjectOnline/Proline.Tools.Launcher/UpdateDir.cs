using System;
using System.Collections.Generic;
using System.IO;

namespace Proline.ServerLauncher.Program
{
    public class GameUpdate
    {
        private ManifestData _manifest;
        private string _dir;
        public GameUpdate(string dir)
        {
            _dir = dir;
        }
    }

    public class UpdateDir : FileDir
    {
        private List<GameUpdate> _updates;

        public UpdateDir(string dir) : base(dir)
        {
            _updates = new List<GameUpdate>();
        }

        internal void AddUpdateDir(string updateDir)
        {
            var path = Path.Combine(DirectoryPath, updateDir);
            var gameUpdate = new GameUpdate(path);
            _updates.Add(gameUpdate);
        }
    }
}
