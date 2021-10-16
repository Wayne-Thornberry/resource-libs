namespace Proline.Common.Logging
{
    public interface ILogMethods
    {
        void LogDebug(string data);
        void LogError(string data);
        void LogInfo(string data);
        void LogWarn(string data);
    }
}