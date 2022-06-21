using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Proline.ModuleFramework.TestModule
{
    public class TestModuleConfigElement : ConfigurationElement
    { 

    }

    public class TestModuleConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Test", IsRequired = true)]
        public string Test { get; set; }  
    }
}
