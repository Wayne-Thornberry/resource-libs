using CitizenFX.Core;
using Proline.Modularization.Core;
using System;
using System.Reflection;
using System.Threading.Tasks; 

namespace Proline.ClassicOnline.MConnection
{
    public partial class ConnectionContext : ModuleScript
    {
        internal PlayerManager _manager;

        public ConnectionContext() 
        {
            _manager = PlayerManager.GetInstance();
        }
    }
}
