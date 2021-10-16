using CitizenFX.Core;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.CFX
{
    public class FXTick : IFXTask
    {
        public async Task Wait(int ms)
        {
            await BaseScript.Delay(ms);
        }
    }
}
