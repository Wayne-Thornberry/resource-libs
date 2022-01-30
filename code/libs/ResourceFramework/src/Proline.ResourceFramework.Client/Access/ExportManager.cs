using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class ExportManager
    {
        private static ExportDictionary _exports;

        internal static void SetExportDictionary(ExportDictionary exports)
        {
            _exports = exports;
        }

        public static ExportDictionary GetExports()
        {
            return _exports;
        }

        public static void CreateExport(string exportName, Delegate deleget)
        {
            _exports.Add(exportName, deleget);
        } 
    }
}
