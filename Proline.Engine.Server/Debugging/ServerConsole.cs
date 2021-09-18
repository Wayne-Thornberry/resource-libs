namespace Proline.Engine.Debugging
{
    internal class ServerConsole : IDebugConsole
    {
        public void Write(object obj, bool replicate)
        { 
            CitizenFX.Core.Debug.Write(obj.ToString()); 
        }

        public void WriteLine(object obj, bool replicate)
        { 
            CitizenFX.Core.Debug.WriteLine(obj.ToString()); 
        } 
    }
}
