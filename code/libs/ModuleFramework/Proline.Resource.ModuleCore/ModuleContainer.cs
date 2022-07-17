using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Modularization.Core
{
    public class ModuleContainer
    {
        public ModuleContainer()
        {
            TaskManager = new List<Task>();
        }

        public bool HasStarted { get; internal set; }
        public bool IsStarted => !IsStartingUp && HasStarted;
        public List<ResourceCommand> Commands { get; set; }
        public List<ModuleScript> Scripts { get; internal set; }
        public AssemblyName Name { get; internal set; }
        public Assembly Assembly { get; internal set; }
        public List<Task> TaskManager { get; internal set; }
        public bool IsStartingUp => StartupScript != null ? !StartupScript.IsFinished : false; 
        public ModuleScript StartupScript { get; internal set; }
    }
}
