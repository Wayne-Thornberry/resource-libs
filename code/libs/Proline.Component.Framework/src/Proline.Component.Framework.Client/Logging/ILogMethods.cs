namespace Proline.Common.Logging
{
    public interface ILogMethods
    {
        void Debug(string data, bool broadcast = false);
        void Error(string data, bool broadcast = false);
        void Info(string data, bool broadcast = false);
        void Warn(string data, bool broadcast = false);
    }
}