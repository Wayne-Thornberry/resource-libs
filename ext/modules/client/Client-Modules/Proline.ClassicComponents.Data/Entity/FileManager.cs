using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Entity
{
    public class FileManager
    {
        private string _memory;
        private static FileManager _instance;

        public static FileManager GetInstance()
        {
            if(_instance == null)
                _instance = new FileManager();
            return _instance;
        }

        internal void PutFileIntoMemory(string data)
        {
            _memory = data;
        }

        internal bool IsInMemory()
        {
            return !string.IsNullOrEmpty(_memory);
        }

        internal string GetFileFromMemory()
        {
            if (IsInMemory())
            {
                var memory = _memory;
                _memory = null;
                return memory;
            }
            return null;
        }
    }
}
