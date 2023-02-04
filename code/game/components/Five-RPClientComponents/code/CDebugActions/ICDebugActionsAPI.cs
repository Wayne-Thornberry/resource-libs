namespace CDebugActions
{
    public interface ICDebugActionsAPI
    {
        void LogDebug(object obj, bool outputToServer = false);
        void LogError(object obj, bool outputToServer = false);
        void LogInfo(object obj, bool outputToServer = false);
        void LogWarn(object obj, bool outputToServer = false);
    }
}