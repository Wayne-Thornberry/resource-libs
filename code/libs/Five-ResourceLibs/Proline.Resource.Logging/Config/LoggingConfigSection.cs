using Proline.Resource.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Logging.Config
{
    public class LoggingConfigSection : ConfigSection
    {   
        public static LoggingConfigSection ModuleConfig => Resource.Configuration.Configuration.GetSection<LoggingConfigSection>("loggingSection");
    }
}
