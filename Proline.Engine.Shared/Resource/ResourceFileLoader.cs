namespace Proline.Engine.Resource
{
    public abstract class ResourceFileLoader
    {
        public ResourceFileLoader()
        {

        }

        public abstract string Load(string resourceName, string filePath); 

    }
}
