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

        public Task<InsertSaveResponse> SaveFile(InsertSaveRequest param)
        {
            return _client.InsertSaveAsync(param);
        }


        public Task<GetSaveResponse> LoadFile(GetSaveRequest param)
        {
            return _client.GetSaveAsync(param); 
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
