using CitizenFX.Core;
using Newtonsoft.Json; 
using Proline.ClassicOnline.MWorld;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class ExitProperty : ResourceCommand
    {
        public ExitProperty() : base("ExitProperty")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length == 3)
            {
                WorldAPI.ExitProperty(args[0].ToString(), args[1].ToString(), args[2].ToString());
            }
        }
    }
}
