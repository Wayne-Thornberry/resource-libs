using Proline.Component.Framework.Client.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Component.CSaving.Client
{
    public static class FileAPI
    {
        public static void UploadData()
        { 
            var exports = ExportManager.GetExports();
            exports["Proline.Component.CSaving"].UploadData();
        }
        public static void AddData(string key, object data)
        { 
            var exports = ExportManager.GetExports();
            exports["Proline.Component.CSaving"].AddData(key,data);
        }
        public static void CreateFile()
        { 
            var exports = ExportManager.GetExports();
            exports["Proline.Component.CSaving"].CreateFile();
        } 
    }
}
