using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerLauncher.Program
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
