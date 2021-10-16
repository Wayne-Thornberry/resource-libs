using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Component.Framework
{
    public abstract class ComponentObject : IComponentObject
    {
        protected ComponentContainer Component => ComponentContainer.GetInstance();
        public ComponentObject()
        {

        } 
    }
}
