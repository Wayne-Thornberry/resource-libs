using System;
using System.IO;
using Newtonsoft.Json;
using Proline.Resource.File;
using Proline.Resource.Logging;

namespace Proline.ResourceConfiguration
{
    public static class ConfigManager
    {
        private static Log _log = new Log();

        public static T GetConfig<T>(string resourceName)
        {
            T config = default;
            try
            { 
                _log.Debug($"Attempting to load config.json in {resourceName}");
                var json = ResourceFile.LoadResourceFile(resourceName, "config.json");
                _log.Debug(json);
                config = JsonConvert.DeserializeObject<T>(json);
                _log.Debug($"Config Loaded Succesfully");
            }
            catch (FileNotFoundException)
            {
                _log.Debug("Config file not found in resource, are you sure config.json exists?");
            }
            catch (Exception)
            {
                _log.Debug("An unkown exception has occured trying to deserialize the config file. Please ensure its formatted correctly"); 
            }
            return config;
        }
    }
}
