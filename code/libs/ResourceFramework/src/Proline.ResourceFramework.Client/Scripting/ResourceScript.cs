using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public abstract class ResourceScript
    {
        public virtual async Task OnStart() { }

        public virtual async Task OnUpdate() { }

        [Tick]
        public async Task OnTick()
        {
            try
            {
                await OnStart();
                await OnUpdate();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
