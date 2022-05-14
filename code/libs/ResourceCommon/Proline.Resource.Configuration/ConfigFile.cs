using Newtonsoft.Json;
using Proline.Resource.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Configuration
{
    public class ConfigFile : ResourceFile
    {
        private IResourceFile _file;
        private Dictionary<string, object> _config;

        public ConfigFile(IResourceFile file)
        {
            _file = file;
        }

        public void LoadConfig()
        {
            _config = JsonConvert.DeserializeObject<Dictionary<string, object>>(_file.GetData());
        }

        public T GetConfigSection<T>(string key)
        {
            if (_config.ContainsKey(key))
                return JsonConvert.DeserializeObject<T>(_config[key].ToString());
            throw new Exception("Config section not found");
        }
    }
}
