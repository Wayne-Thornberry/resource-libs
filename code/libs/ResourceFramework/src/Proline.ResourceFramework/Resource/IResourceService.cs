namespace Proline.ResourceFramework
{
    public interface IResourceService
    {
        void AddAPI<T>();
        void AddAPI<T>(string v);
        void AddControllers();
        void AddEvents();
        void AddExports();
        void AddScripts();
    }
}