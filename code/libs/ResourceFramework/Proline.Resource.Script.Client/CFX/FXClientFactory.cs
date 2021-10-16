using CitizenFX.Core.Native;
using Proline.Resource.CFX; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Script.CFX
{
    public class FXClientFactory : CFXFactory
    {
        public override CFXObject Build<T>()
        {
            switch (typeof(T).Name)
            {
                case "IFXConsole": return new FXConsole();
                case "IFXEventHandler": return null;
                case "IFXEventTrigger": return new FXEventTrigger();
                case "IFXResource": return new FXResource(API.GetCurrentResourceName());
                case "IFXTask": return new FXTask(); 
            }
            return null;
        }
    }
}
