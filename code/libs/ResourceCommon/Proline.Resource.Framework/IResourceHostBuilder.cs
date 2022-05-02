namespace Proline.Resource.Framework
{
    public interface IResourceHostBuilder
    {
        IResourceHost Build();
        IResourceHostBuilder UseSetting(string key, string value);
    }
}