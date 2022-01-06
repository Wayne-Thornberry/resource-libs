using Newtonsoft.Json;
using Proline.Resource.Client.Common;
using Proline.Resource.Client.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Configuration
{
    public static class ResourceConfig
    {
        private static IResourceConfiguration _config;

        public static T LoadResourceConfig<T>() where T : IResourceConfiguration
        {
            var data = FileLoader.LoadFile("resource.json");
            if (string.IsNullOrEmpty(data))
                return default;
            _config = JsonConvert.DeserializeObject<T>(data);
            return (T) _config;
        }

        public static T GetResourceConfig<T>() where T : IResourceConfiguration
        {
            if (_config != null)
                return (T) _config;
            else
                return LoadResourceConfig<T>();
        }
    }
}
