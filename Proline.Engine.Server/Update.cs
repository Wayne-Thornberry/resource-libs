
using CitizenFX.Core;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Online.Script
{
    public partial class EngineScript : BaseScript
    {

        private async Task Update()
        {

        }

        private async Task ProcessAPIRequests()
        {
            try
            {
                //_engine.LogDebug(_header, "Processing API Requests");
                //var httpClient = new HttpClient();
                //var re = await httpClient.GetAsync("http://localhost:1125/NativeCalls/GetIncompleteNativeCalls");
                //var nativeCalls = JsonConvert.DeserializeObject<List<NativeCall>>(await re.Content.ReadAsStringAsync());
                //_engine.LogDebug(_header, nativeCalls.Count);

                //foreach (var item in nativeCalls)
                //{
                //    item.State = (int) NativeCallState.COMPLETED;
                //    item.ReturnResult = "1";
                //    item.CallCompleted = DateTime.UtcNow;
                //}


                //var stringContent = new StringContent(JsonConvert.SerializeObject(nativeCalls), Encoding.UTF8, "application/json");
                //re = await httpClient.PostAsync("http://localhost:1125/NativeCalls/UpdateNativeCalls", stringContent);
                //var responses = JsonConvert.DeserializeObject<List<NativeCall>>(await re.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            { 

            }
        }
    }
}
