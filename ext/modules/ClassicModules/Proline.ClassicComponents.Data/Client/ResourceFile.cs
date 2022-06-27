using Proline.ClassicOnline.MDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    public partial class API
    {
        public static string LoadResourceFile(string fileName)
        {
            try
            {
                return LoadResourceFile(CitizenFX.Core.Native.API.GetCurrentResourceName(), fileName);
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return null;
        }

        public static string LoadResourceFile(string resourceName, string fileName)
        {
            try
            {
                return CitizenFX.Core.Native.API.LoadResourceFile(resourceName, fileName);
            }
            catch (Exception e)
            {
                MDebugAPI.LogDebug(e.ToString());
            }
            return null;
        }
    }
}
