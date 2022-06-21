using CitizenFX.Core;
using Proline.Resource.Scripting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.Modularization.Core
{
    public abstract class ModuleScript : ExtendedScript
    {
        public string Name { get; private set; }
        public ModuleState State { get; private set; }

        public ModuleScript(Assembly source) : base(false)
        {
            State = ModuleState.Created;
            Name = source.GetName().Name;
        }

        public override async Task OnLoad()
        {

            try
            {
                State = ModuleState.Loading;
                // could look for events outside of script
                // - this includes callback events
                // could look for exports
                // could look for commands
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                State = ModuleState.Active;
            }
        }

    }
}
