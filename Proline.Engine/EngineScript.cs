using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public abstract class EngineScript : EngineObject
    {
        protected EngineScript() : base("Component")
        {
        }

        public int Status { get; set; }

        public virtual void Start() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnEngineEvent(string eventName, params object[] args) { }
    }
}
