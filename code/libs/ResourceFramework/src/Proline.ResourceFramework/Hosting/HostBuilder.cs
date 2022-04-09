using Proline.ResourceFramework.Common;
using Proline.ResourceFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework
{
    public class HostBuilder : IHostBuilder
    {
        private string[] args;

        internal HostBuilder(string[] args)
        {
            this.args = args;
            Properties = new Dictionary<object, object>();
        }

        public IDictionary<object, object> Properties { get; }

        public IHost Build()
        {
            return new Host((IResourceHost)Properties["ResourceHost"]);
        }
    }
}
