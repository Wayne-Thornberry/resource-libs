using Proline.Resource;
using Proline.Resource.Logging;
using System;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MDebug
{
    public static class MDebugAPI
    {
        private static Log _log => new Log();

        public static void LogDebug(object data, bool broadcast = false)
        {
            try
            { 
                // Log in memory
                var line = _log.Debug(data.ToString());
                // Output to console
                Console.WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void LogDebug(string data, bool broadcast = false)
        {
            try
            { 
                // Log in memory
                var line = _log.Debug(data);
                // Output to console
                Console.WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void LogWarn(string data, bool broadcast = false)
        {
            try
            { 
                // Log in memory
                var line = _log.Warn(data.ToString());
                // Output to console
                Console.WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void LogInfo(string data, bool broadcast = false)
        {
            try
            { 
                // Log in memory
                var line = _log.Info(data.ToString());
                // Output to console
                Console.WriteLine(line);
                // Duplciate to server
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void LogError(string data, bool broadcast = false)
        {
            try
            { 
                // Log in memory
                var line = _log.Error(data.ToString());
                // Output to console
                Console.WriteLine(line);
                // Duplciate to server
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}
