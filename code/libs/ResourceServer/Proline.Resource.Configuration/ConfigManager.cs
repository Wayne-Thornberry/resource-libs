using System;
using System.Collections.Generic;
using System.IO;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Resource.File;

namespace Proline.Resource.Configuration
{
    public static class ConfigManager
    {
        //private static Log _log = new Log(); 


        public static T GetResourceConfigSection<T>(string resourceName, string key)
        {
            Dictionary<string, object> config = null;
            try
            {
                //   _log.Debug($"Attempting to load config.json in {key}");
                var file = ResourceFile.LoadResourceFile(resourceName, "config.json");
                // _log.Debug(json);
                config = JsonConvert.DeserializeObject<Dictionary<string, object>>(file.GetData());
                //_log.Debug($"Config Loaded Succesfully");
            }
            catch (FileNotFoundException)
            {
                //   _log.Debug("Config file not found in resource, are you sure config.json exists?");
            }
            catch (Exception)
            {
                //  _log.Debug("An unkown exception has occured trying to deserialize the config file. Please ensure its formatted correctly");
            }
            return JsonConvert.DeserializeObject<T>(config[key].ToString());
        }

        public static T GetResourceConfigSection<T>(string key)
        {
            Dictionary<string, object> config = null;
            try
            {
                //   _log.Debug($"Attempting to load config.json in {key}");
                var file = ResourceFile.LoadResourceFile(API.GetCurrentResourceName(), "config.json");
                // _log.Debug(json);
                config = JsonConvert.DeserializeObject<Dictionary<string, object>>(file.GetData());
                //_log.Debug($"Config Loaded Succesfully");
            }
            catch (FileNotFoundException)
            {
                //   _log.Debug("Config file not found in resource, are you sure config.json exists?");
            }
            catch (Exception)
            {
                //  _log.Debug("An unkown exception has occured trying to deserialize the config file. Please ensure its formatted correctly");
            }
            return JsonConvert.DeserializeObject<T>(config[key].ToString());
        }
    }
}
