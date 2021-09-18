namespace Proline.Engine.Resource
{
    public class ClientFileLoader : ResourceFileLoader
    {
        public override string Load(string resourceName, string filePath)
        { 
            return CitizenFX.Core.Native.API.LoadResourceFile(resourceName, filePath);
        }
    }
}
