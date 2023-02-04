namespace CCoreSystem.Internal
{
    internal class InvokedEvent
    {
        public InvokedEvent(string eventName, object[] args)
        {
            EventName = eventName;
            Args = args;
        }

        public string EventName { get; set; }
        public object[] Args { get; set; }
    }
}