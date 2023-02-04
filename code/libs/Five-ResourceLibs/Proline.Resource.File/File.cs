using System;
using System.IO;
using CitizenFX.Core.Native;

namespace Proline.Resource.IO
{
    public abstract class File
    {
        private string _path;
        private string _resource;
        private string _data;

        public string Resource => _resource;
        public string Path => _path;

        public File(string resource, string path)
        {
            _path = path;
            _resource = resource;
        }

        public string Load()
        {
            try
            {
                _data = API.LoadResourceFile(_resource, _path);
                if (_data == null)
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
            return _data;
        }

        public string GetData()
        {
            if (_data == null)
                Load();
            return _data;
        }
    }
}
