namespace Proline.Engine.Internals
{
    public static class EngineStatus
    {
        public static bool IsExtensionsInitialized { get; set; }
        public static bool IsScriptsInitialized { get; set; }
        public static bool IsComponentsInitialized { get; set; }
        public static bool IsEngineInitialized { get; set; }
    }
}
