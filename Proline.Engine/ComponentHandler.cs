using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class ComponentHandler
    {
        public virtual void OnComponentInitialized()
        {

        }

        public virtual void OnComponentLoad()
        {

        }

        public virtual void OnComponentEvent(string eventName, params object[] args)
        {

        }

        public virtual void OnComponentStart()
        {

        }

        public virtual void OnComponentStop()
        {

        }
    }
}
