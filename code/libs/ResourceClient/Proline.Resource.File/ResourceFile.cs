using System;
using System.IO;
using CitizenFX.Core.Native;
using Proline.Resource.Logging;

namespace Proline.Resource.File
{
    public static class ResourceFile
    {
        private static Log _log = new Log();
        public static string LoadResourceFile(string resourceName, string fileName)
        {
            try
            {
                _log.Debug("Loading file " + fileName + " from resource " + resourceName);
                var data = API.LoadResourceFile(resourceName, fileName);
                if (string.IsNullOrEmpty(data))
                {
                    throw new FileNotFoundException();
                }
                else
                {
                    return data;
                }
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                _log.Debug($"Unkown exception caught when trying to load resource file {fileName} in resource {resourceName}");
            }
            return "";
        }
    }
}
