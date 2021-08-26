
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proline.Freemode.Components.CInstancing
{
    public class InstanceAPI : ComponentAPI
    {
        [Client]
        [ComponentAPI]
        public void LeaveInstance()
        {
            // Do some saving
            //var response = ExecuteServerMethod("LeaveInstance", null);
            return;
        }
    }
}
