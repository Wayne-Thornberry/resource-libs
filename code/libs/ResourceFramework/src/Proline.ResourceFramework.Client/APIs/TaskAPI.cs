using CitizenFX.Core;
using Proline.ResourceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.APIs
{
    public class TaskAPI : IFiveAPI, ITaskMethods
    { 
        public async Task Delay(int ms)
        {
            await BaseScript.Delay(ms);
        }
    }
}
