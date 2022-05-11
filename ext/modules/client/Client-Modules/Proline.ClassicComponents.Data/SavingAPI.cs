using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proline.Resource.Networking;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.MData
{
    public static class SavingAPI
    {
        public static void SaveObject(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            BaseScript.TriggerServerEvent("SaveObject", json);
        }
    }
}
