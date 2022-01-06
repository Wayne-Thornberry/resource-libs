using Newtonsoft.Json;
using Proline.Online.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Online.Client
{
    public class OnlineClient : HttpClient
    {
        public void InsertUser(string userName)
        {
            var data = JsonConvert.SerializeObject(new UserInParameter() { Username = userName });
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var re = this.PostAsync("http://localhost:1125/User/PutUser/", stringContent).Result;
        }

            //var native = JsonConvert.DeserializeObject<APINative>(await re.Content.ReadAsStringAsync());

            //var call = new NativeCall
            //{
            //    NativeId = native.NativeId,
            //    CallCreated = DateTime.UtcNow,
            //    CallUpdated = DateTime.UtcNow,
            //    CallCompleted = DateTime.MinValue,
            //    InArgs = null,
            //    OutArgs = null,
            //};
            //var data = JsonConvert.SerializeObject(call);
            //var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            //re = await httpClient.PostAsync("http://localhost:1125/NativeCalls/AddNativeCall", stringContent);
            //call = JsonConvert.DeserializeObject<NativeCall>(await re.Content.ReadAsStringAsync());

            //while (!call.HasCallCompleted)
            //{
            //    re = await httpClient.GetAsync("http://localhost:1125/NativeCalls/" + call.CallId);
            //    call = JsonConvert.DeserializeObject<NativeCall>(await re.Content.ReadAsStringAsync());
            //    var totalTime = DateTime.UtcNow.Subtract(call.CallCreated).TotalSeconds;
            //    if (totalTime > 30)
            //    {
            //        call.HasCallCompleted = true;
            //        stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            //        re = await httpClient.PutAsync("http://localhost:1125/NativeCalls/" + call.NativeId, stringContent);
            //        throw new TimeoutException();
            //    }
            //    Thread.Sleep(1000);
            //}

            //if (string.IsNullOrEmpty(call.ReturnResult))
            //    return default;
            //if (typeof(T).IsPrimitive)
            //    return (T)Convert.ChangeType(call.ReturnResult, typeof(T));
          //  return; //JsonConvert.DeserializeObject<T>(call.ReturnResult);
        //}
    }
}
