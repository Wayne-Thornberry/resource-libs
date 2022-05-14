using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.File
{
    public interface IResourceFile
    {
        void Load();
        string GetData();
    }
}
