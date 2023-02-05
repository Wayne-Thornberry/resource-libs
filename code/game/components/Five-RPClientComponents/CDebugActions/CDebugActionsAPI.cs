using Proline.Resource.Eventing;
using Proline.Resource.Logging; 
using System;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace CDebugActions
{
    public class CDebugActionsAPI : ICDebugActionsAPI
    {
        private static Log _log => new Log();

        public void LogDebug(object obj, bool outputToServer = false)
        {
            try
            {
                // Log in memory
                var line = _log.Debug(obj.ToString());
                // Output to console
                Console.WriteLine(line);
                if (outputToServer)
                    WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LogWarn(object obj, bool outputToServer = false)
        {
            try
            {
                // Log in memory
                var line = _log.Warn(obj.ToString());
                // Output to console
                Console.WriteLine(line);
                if (outputToServer)
                    WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LogInfo(object obj, bool outputToServer = false)
        {
            try
            {
                // Log in memory
                var line = _log.Info(obj.ToString());
                // Output to console
                Console.WriteLine(line);
                if (outputToServer)
                    WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LogError(object obj, bool outputToServer = false)
        {
            try
            {
                // Log in memory
                var line = _log.Error(obj.ToString());
                // Output to console
                Console.WriteLine(line);
                if (outputToServer)
                    WriteLine(line);
                // Duplciate to server
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static async Task<string> WriteLine(string data)
        {
            try
            { 
                var x = new EventCaller("ConsoleWriteLineHandler");
                x.OnEventCallback((args) =>
                {

                });
                x.Invoke(data);
                await x.WaitForCallback();
                return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static async Task<string> Write(string data)
        {
            try
            {
                var x = new EventCaller("ConsoleWriteHandler");
                x.OnEventCallback((args) =>
                {

                });
                x.Invoke(data);
                await x.WaitForCallback();
                return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
