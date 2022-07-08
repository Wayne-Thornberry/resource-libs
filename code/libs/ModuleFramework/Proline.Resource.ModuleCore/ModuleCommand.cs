using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;
using Proline.Resource.Framework;

namespace Proline.Modularization.Core
{
    public abstract class ModuleCommand : ResourceCommand
    {

        public ModuleCommand(string commandName) : base(commandName)
        {

        }
    }
}
