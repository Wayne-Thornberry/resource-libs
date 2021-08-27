using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ResourceCache
    {
        private Dictionary<string, string> _fileData;

        internal ResourceCache()
        {
            _fileData = new Dictionary<string, string>();
        }

        internal bool ContainsFile(string fileName)
        {
            return _fileData.ContainsKey(fileName);
        }

        internal string GetFileData(string fileName)
        {
            return _fileData[fileName];
        }

        internal void CacheFileData(string fileName, string data)
        {
            ///TODO - Encrpt or compress the data to save on space
            _fileData.Add(fileName, data);
        }
    }
}
