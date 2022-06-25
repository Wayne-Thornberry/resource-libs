namespace Proline.Resource.Eventing
{
    public class EventTriggered
    {
        public string CallBackEvent { get; set; }
        public object[] Args { get; set; }
    }

    public class CallbackEventRequest : EventTriggered
    {
        public string CallBackEvent { get; set; }
    }
}

