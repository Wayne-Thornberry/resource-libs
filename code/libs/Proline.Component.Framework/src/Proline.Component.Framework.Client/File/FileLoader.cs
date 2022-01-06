using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Client.Common
{
    internal static class FileLoader
    {
        internal static string LoadFile(string fileName)
        {
            return API.LoadResourceFile(API.GetCurrentResourceName(), fileName);
        }
    }
}
