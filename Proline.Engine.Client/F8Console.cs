using CitizenFX.Core;
using Proline.Engine.Debugging;

namespace Proline.Engine
{
    public static class F8Console
    {
        /// <summary>
        /// Calls to execute a command in the FiveM console
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        public static void ExecuteCommand(string command, params string[] args)
        {

        }

        /// <summary>
        /// Writes the toString value of an object into the console with no line break
        /// </summary>
        /// <param name="obj"></param>
        public static void Write(object obj)
        {
            var console = new DebugConsole();
            console.Write(obj.ToString(), false);
        }
        /// <summary>
        /// Writes the toString value of an object into the console with a line break
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteLine(object obj)
        {
            var console = new DebugConsole();
            console.WriteLine(obj.ToString(), false);
        } 
    }
}
