using System;
using System.Collections.Generic;
using System.IO;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Resource.IO;

namespace Proline.Resource.Configuration
{
    public static class Configuration
    {
        //private static Log _log = new Log(); 


        public static T GetSection<T>(string resourceName, string key)
        {
            Dictionary<string, object> config = null;
            try
            {
                //   _log.Debug($"Attempting to load config.json in {key}");
                var file = ResourceFile.LoadResourceFile(resourceName, "config.json");
                Console.WriteLine(file.GetData());
                if (file == null)
                    throw new FileNotFoundException("Config file was not found");
                var cp = new ConfigProcessor();
                var cf = cp.TransformFileIntoConfig(file);
                // _log.Debug(json);
                return cf.GetConfigSection<T>(key);
                //_log.Debug($"Config Loaded Succesfully");
            }
            catch (FileNotFoundException)
            {
                throw new ConfigFileNotFoundException();
                //   _log.Debug("Config file not found in resource, are you sure config.json exists?");
            }
            catch (Exception)
            {
                //  _log.Debug("An unkown exception has occured trying to deserialize the config file. Please ensure its formatted correctly");
            }
            return default;
        }

        public static T GetSection<T>(string key)
        {
            Dictionary<string, object> config = null;
            try
            {
                //   _log.Debug($"Attempting to load config.json in {key}");
                var file = ResourceFile.LoadResourceFile(API.GetCurrentResourceName(), "config.json");
                Console.WriteLine(file.GetData());
                if (file == null)
                    throw new FileNotFoundException("Config file was not found");
                var cp = new ConfigProcessor();
                var cf = cp.TransformFileIntoConfig(file);

                var sections = cf.GetConfigSection<List<string>>("Sections");
                if (sections.Contains(key))
                {
                    return cf.GetConfigSection<T>(key);
                }

                // _log.Debug(json);
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
            return default;
        }
    }
}
