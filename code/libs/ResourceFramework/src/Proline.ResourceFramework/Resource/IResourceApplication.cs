using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework
{
    public interface IResourceApplication
    {
        void BindConfiguration<T>();
    }
}
