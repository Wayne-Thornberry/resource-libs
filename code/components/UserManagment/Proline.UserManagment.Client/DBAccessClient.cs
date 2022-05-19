using Proline.DBAccess.Proxies; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.Proxies
{
    public class DBAccessClient : IDisposable
    {
        private AuthenticationHeaderValue _authHeader;
        private HttpClient httpClient;
        private DBAccessApi _client;

        public DBAccessClient()
        {
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = _authHeader;
            _client = new DBAccessApi("http://localhost:9703/", httpClient);
        }

        public async Task SaveFile(PlacePlayerDataInParameters param)
        {
            await _client.SaveFileAsync(param);
        }


        public async Task<GetPlayerDataInParameters> LoadFile(GetPlayerDataInParameters param)
        {
            var x = await _client.LoadFileAsync(param);
            return x;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
