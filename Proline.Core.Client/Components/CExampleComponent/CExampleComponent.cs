using Proline.Engine;
using Proline.Engine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Server.Components
{
    public class CExampleComponent : ComponentHandler
    {
        public override void OnInitialized()
        {
            base.OnInitialized();
        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        public override void OnStart()
        {
           // EngineAccess.ExecuteComponentAPI(this, "ExampleControl", new object[] { "X", "Y", "X" });
            base.OnStart();
        }

        public override void OnStop()
        {
            base.OnStop();
        }

        [EngineCommand("X")]
        public void ExampleCommand()
        {

        }

        [ComponentAPI]
        public void ExampleControl(string x, string y, string z)
        {
            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
        }

        [ComponentAPI]
        public void PlayerAPI(string userName)
        {
            //try
            //{ 
            //    var client = new OnlineClient();
            //    client.InsertUser(userName);
            //}
            //catch (Exception e)
            //{
            //    Debugger.LogDebug("Client error, probably offline, dunno lol");
                
            //}
        }
    }
}
