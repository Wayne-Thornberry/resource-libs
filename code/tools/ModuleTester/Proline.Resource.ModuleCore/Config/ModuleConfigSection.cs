using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ModuleFramework.Core.Config
{
    public class ModuleConfigSection : ConfigurationSection
    {
        public static ModuleConfigSection ModuleConfig => (ModuleConfigSection) ConfigurationManager.GetSection("moduleConfigSection");

        // Create a property that lets us access the collection
        // of SageCRMInstanceElements

        // Specify the name of the element used for the property
        [ConfigurationProperty("moduleInstances")]
        // Specify the type of elements found in the collection
        [ConfigurationCollection(typeof(ModuleInstanceCollection))]
        public ModuleInstanceCollection Modules
        {
            get
            {
                // Get the collection and parse it
                return (ModuleInstanceCollection)this["moduleInstances"];
            }
        }
    }
}
