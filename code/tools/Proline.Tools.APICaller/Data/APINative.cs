namespace Proline.ProlineEditor.APICaller
{
    public class APINative
    {
        public long NativeId { get; set; }
        public string NativeName { get; set; }
        public bool HasReturnType { get; set; }
        public string NativeReturnType { get; set; }
        public string NativeHash { get; set; }
        public int NativeArgCount { get; set; }
        public string NativeArgs { get; set; }
        public int NativeType { get; set; }
    }
}
