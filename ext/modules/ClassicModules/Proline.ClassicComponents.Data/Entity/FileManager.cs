using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Entity
{
    public class FileManager
    {
        private static FileManager _instance;
        private Dictionary<string, object> _file; 
        public bool IsSaveInProgress { get; internal set; } 
        public int LastSaveResult { get; internal set; }

        public static FileManager GetInstance()
        {
            if(_instance == null)
                _instance = new FileManager();
            return _instance;
        }

        internal void PutFileIntoMemory(string data)
        {
            _file = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
        }

        internal bool IsInMemory()
        {
            return _file != null;
        }

        internal Dictionary<string, object> GetCurrentSaveFile()
        {
            return _file;
        }

        internal void CreateNewFile()
        {
            _file = new Dictionary<string, object>();
        }
    }
}
