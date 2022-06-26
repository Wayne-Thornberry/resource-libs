namespace Proline.Resource.Eventing
{
    public class EventTriggered
    {
        public bool IsCallback { get; set; }
        public bool HasCallback { get; set; } 
        public object[] Args { get; set; }
    }
}

