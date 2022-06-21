using System;
using System.IO;
using CitizenFX.Core.Native;

namespace Proline.Resource.IO
{
    public class ResourceFile : File
    {
        public ResourceFile(string resource, string path) : base(resource, path)
        {

        }

        // private static Log _log = new Log();
        public static ResourceFile LoadResourceFile(string resourceName, string fileName)
        {
            try
            {
                // _log.Debug("Loading file " + fileName + " from resource " + resourceName); 
                var file = new ResourceFile(resourceName, fileName);
                file.Load();
                return file;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                //  _log.Debug($"Unkown exception caught when trying to load resource file {fileName} in resource {resourceName}");
            }
            return null;
        }

        public static ResourceFile LoadCurrentResourceFile(string fileName)
        {
            try
            {
                // _log.Debug("Loading file " + fileName + " from resource " + resourceName);
                var resourceName = API.GetCurrentResourceName();
                return LoadResourceFile(resourceName, fileName);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                //  _log.Debug($"Unkown exception caught when trying to load resource file {fileName} in resource {resourceName}");
            }
            return null;
        }
    }
}
