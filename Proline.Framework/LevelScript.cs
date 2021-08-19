using System.Threading.Tasks;

namespace Proline.Framework
{
    internal enum ScriptExitCode : int
    {
        SUCCESS = 0,
        ERROR = 1,
    }

    public abstract class LevelScript : IEngineTracker
    {
        protected LevelScript()
        {
            Name = this.GetType().Name;
            Type = "LevelScript";
        }

        private string FullName { get ; set; }
        private long Ticks { get; set ; }
        private int Handle { get; set ; }
        private bool IsMonoScript { get; set ; }
        private Task ExecutingTask { get; set; }
        public object[] Parameters { get; set; }
        public int Stage { get; set; }

        public string Name { get; set; }

        public string Type { get; }
        public abstract Task Execute(params object[] args);
    }
}
