using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Framework
{
    public abstract class SimpleComponent : IEngineTracker
    {
        public string Name { get; }
        public string Type { get; }

        protected SimpleComponent()
        {
            Name = this.GetType().Name;
            Type = "Component";
        }

        public virtual void OnComponentInitialized()
        {

        }

        public virtual void OnComponentLoad()
        {

        }

        public virtual void OnComponentStart()
        {

        }

        public virtual void OnComponentStop()
        {

        }

        public virtual async Task Tick()
        {

        }
    }
}
