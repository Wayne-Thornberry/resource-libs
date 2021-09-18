namespace Proline.Engine.Networking
{
    internal class EventRequestParams
    {
        public string GUID { get; set; }
        public string ComponentName { get; set; }
        public string MethodName { get; set; }
        public string MethodArgs { get; set; }
    }
}
