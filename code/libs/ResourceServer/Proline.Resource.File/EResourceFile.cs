using System;
using System.IO;
using CitizenFX.Core.Native; 

namespace Proline.Resource.File
{
    public class EResourceFile : ResourceFile, IResourceFile
    {
        private string _path;
        private string _resource;
        private string _data;

        private EResourceFile(string resource, string path)
        {
            _path = path;
            _resource = resource;
        }

        public void Load()
        {
            try
            { 
                _data = API.LoadResourceFile(_resource, _path);
                if(_data == null)
                {
                    throw new FileNotFoundException();
                }
            }
            catch (FileNotFoundException)
            { 
                throw;
            }
            catch (Exception)
            {

            }
        }

        public string GetData()
        {
            if (_data == null)
                Load();
           return _data;
        }
         

        // private static Log _log = new Log();
        public static EResourceFile LoadResourceFile(string resourceName, string fileName)
        {
            try
            {
                // _log.Debug("Loading file " + fileName + " from resource " + resourceName); 
                var file = new EResourceFile(resourceName, fileName);
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

        public static EResourceFile LoadCurrentResourceFile(string fileName)
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
