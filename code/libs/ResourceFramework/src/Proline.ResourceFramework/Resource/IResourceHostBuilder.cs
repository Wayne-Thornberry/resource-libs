namespace Proline.ResourceFramework.Extensions
{
    public interface IResourceHostBuilder
    {
        IResourceHost Build();
        IResourceHostBuilder UseSetting(string key, string value);
    }
}