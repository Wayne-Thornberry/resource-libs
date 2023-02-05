using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Configuration
{
    public class ConfigSection : Dictionary<string, object>
    {
        public T GetConfigSection<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(this[key].ToString());
        }

        public bool DoesConfigSectionExist(string key)
        {
            return ContainsKey(key);
        }
    }
}
