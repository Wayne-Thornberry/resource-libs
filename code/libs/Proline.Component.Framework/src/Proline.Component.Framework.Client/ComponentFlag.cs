using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Component
{
    [Flags]
    public enum ComponentFlag : byte
    {
        SYNCED, // A flag to indicate that properties contained in the 'component properties' should be sycned 
    }
}
