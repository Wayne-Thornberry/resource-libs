namespace Proline.Resource.Eventing
{
    public class EventTriggered
    {
        public string EventName { get; set; }
        public string Source { get; set; }
        public bool IsInternal { get; set; }
        public bool IsCallback { get; set; }
        public bool HasCallback { get; set; }
        public string CallbackEventName { get; set; }
        public int EventType { get; set; }
        public object[] Args { get; set; }
    }
}

