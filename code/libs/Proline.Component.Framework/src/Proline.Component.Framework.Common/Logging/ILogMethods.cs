namespace Proline.Common.Logging
{
    public interface ILogMethods
    {
        void Debug(string data);
        void Error(string data);
        void Info(string data);
        void Warn(string data);
    }
}