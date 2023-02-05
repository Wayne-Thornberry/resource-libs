using Newtonsoft.Json;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Configuration
{
    public class ConfigFile : ResourceFile
    {
        private Dictionary<string, object> _config;

        public ConfigFile(string resource, string path) : base(resource, path)
        {

        }

        public void LoadConfig()
        {
            _config = JsonConvert.DeserializeObject<Dictionary<string, object>>(GetData());
        }

        public T GetConfigSection<T>(string key)
        {
            if (_config.ContainsKey(key))
                return JsonConvert.DeserializeObject<T>(_config[key].ToString());
            throw new Exception("Config section not found");
        }
    }
}
