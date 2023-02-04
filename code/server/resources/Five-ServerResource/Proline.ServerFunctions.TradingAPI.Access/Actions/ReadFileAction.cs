using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.Resource.Eventing;
using Proline.ServerAccess.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ServerAccess.IO.Actions
{
    public class ReadFileAction : ExtendedEvent
    {   
        public string Data { get; set; }

        public ReadFileAction() : base(EventNameDefinitions.LoadFileHandler, true)
        {

        }  
        protected override void OnEventCallback(params object[] args)
        {
            if(args != null)
            {
                // old way via getting id
                if (args.Length > 0)
                { 
                    if(args[0] != null)
                        Data = args[0].ToString();
                }
            }
        } 
    }
}
