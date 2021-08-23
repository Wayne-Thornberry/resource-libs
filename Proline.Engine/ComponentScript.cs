using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public abstract class ComponentScript
    {
        public int Status { get; set; }

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnEngineEvent(string eventName, params object[] args) { }
    }
}
