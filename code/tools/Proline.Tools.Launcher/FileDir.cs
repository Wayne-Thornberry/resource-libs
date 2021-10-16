using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Online.Launcher
{
    public class FileDir
    {
        private string _dir;

        protected string DirectoryPath => _dir;

        public FileDir(string dir)
        {
            _dir = dir;
        }
    }
}
