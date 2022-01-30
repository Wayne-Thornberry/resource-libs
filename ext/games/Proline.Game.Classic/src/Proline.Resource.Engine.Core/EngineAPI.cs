using CitizenFX.Core;
using Proline.Resource.Client.Logging;
using Proline.Resource.Component.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Engine.Core
{
    public static class EngineAPI
    {
        public static bool IsPointWithinActivationRange(Vector3 position)
        {
            //var exports = ExportManager.GetExports();
            //return exports["Proline.Component.CScriptAreas"].IsPointWithinActivationRange(position);
            return World.GetDistance(position, Game.PlayerPed.Position) < 10f;
        }

        public static void StartNewScript(string name, params object[] args)
        {
            //var exports = ExportManager.GetExports();
            //exports["Proline.Component.CScripting"].StartScript(name, args); 
            EventManager.InvokeEventV2("StartScriptHandler", name, args);
        }

        public static void UploadFile()
        {
            //var exports = ExportManager.GetExports();
            //exports["Proline.Component.CSaving"].UploadData();
            EventManager.InvokeEventV2("UploadFile");
        }
        public static void AddData(string key, object data)
        {
            //var exports = ExportManager.GetExports();
            //exports["Proline.Component.CSaving"].AddData(key, data);
            EventManager.InvokeEventV2("AddData", key, data);
        }
        public static void CreateFile()
        {
            //var exports = ExportManager.GetExports();
            //exports["Proline.Component.CSaving"].CreateFile();
            EventManager.InvokeEventV2("CreateFile");
        }


    }
}
