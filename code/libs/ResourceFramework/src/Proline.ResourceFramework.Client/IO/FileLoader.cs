using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.IO
{
    internal static class FileLoader
    {
        internal static string LoadFile(string fileName)
        {
            return API.LoadResourceFile(API.GetCurrentResourceName(), fileName);
        }
    }
}
