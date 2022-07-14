﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Commands
{
    public class StartNewScriptCommand : ModuleCommand
    {
        public StartNewScriptCommand() : base("StartNewScript")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Count() == 0)
            {
                return;
            }
            var scriptName = args[0].ToString();
            MScriptingAPI.StartNewScript(scriptName);
        } 
    }
}