using CitizenFX.Core.Native;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace Proline.ExampleClient.DataComponent
{
    public static class ResourceFile
    {
        public static object GetFileValue(string file, string key)
        {
            dynamic x = JsonConvert.DeserializeObject<ExpandoObject>(API.LoadResourceFile(API.GetCurrentResourceName(), file));
            return x.Money;
        }
    }
}
