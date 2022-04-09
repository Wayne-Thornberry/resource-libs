using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework
{
    public class ResourceApplication : IResourceApplication
    {
        private Type _configType;

        public void BindConfiguration<T>()
        {
            _configType = typeof(T);
        }
    }
}
