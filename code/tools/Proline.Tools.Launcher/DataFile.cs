using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Online.Launcher
{
    public class DataFile<T>
    {
        public T DataObject { get; }
        public int LoadedTime { get; }
        public DateTime DateLoaded { get; }
        public string FileName { get; } 
        public string Data { get; }

        public DataFile()
        {

        }
    }
}
