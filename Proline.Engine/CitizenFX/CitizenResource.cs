using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class CitizenResource
    {
        public string Name { get; }

        private ResourceCache _cache;

        internal CitizenResource(string resourceName)
        {
            Name = resourceName;
            _cache = new ResourceCache();
        }

        internal string LoadFile(string fileName)
        {
            if (_cache.ContainsFile(fileName))
            {
               return _cache.GetFileData(fileName);
            }
            else
            {
                var data = ResourceFile.Load(this, fileName);
                _cache.CacheFileData(fileName, data);
                return data;
            }
        }

    }
}
