using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.IO
{
    public class ResourceFileLoader
    {
        public string Load(string filePath)
        { 
           return FileLoader.LoadFile(filePath);
        }
    }
}
