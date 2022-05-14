using Proline.Proxies.UserManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Proxies.UserManagment
{
    public class EngineClient : IDisposable
    {
        private AuthenticationHeaderValue _authHeader;
        private HttpClient httpClient;
        private Client _client;

        public EngineClient()
        {
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            _client = new Client("http://localhost:9703/", httpClient);
        }

        public void LoginPlayer(LoginPlayerInParameter param)
        {
            var x = _client.LoginPlayerAsync(param).Result;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
