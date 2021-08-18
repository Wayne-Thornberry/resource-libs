namespace Proline.Engine.Networking
{
    public class MethodCall
    {
        public string ComponentName { get; set; }
        public string MethodName { get; set; }
        public object[] MethodArgs { get; set; }
        public object ReturnResult { get; set; }
    }
}