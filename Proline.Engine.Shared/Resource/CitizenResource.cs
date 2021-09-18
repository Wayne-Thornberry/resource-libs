namespace Proline.Engine.Resource
{
    public class CitizenResource
    {
        public string Name { get; }

        private ResourceCache _cache;
        private ResourceFileLoader _loader;

        public CitizenResource(ResourceFileLoader loader, string resourceName)
        {
            Name = resourceName;
            _cache = new ResourceCache();
        }

        internal string LoadFile(string fileName)
        {
            if (_cache.ContainsFile(fileName))
            {
               return _cache.GetFileData(fileName);
            }
            else
            {
                var data = _loader.Load(Name, fileName);
                _cache.CacheFileData(fileName, data);
                return data;
            }
        }

    }
}
