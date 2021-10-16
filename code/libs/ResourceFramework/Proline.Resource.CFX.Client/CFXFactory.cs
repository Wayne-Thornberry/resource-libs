using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.CFX
{
    public abstract class CFXFactory
    {
        public abstract CFXObject Build<T>();
    }
}
