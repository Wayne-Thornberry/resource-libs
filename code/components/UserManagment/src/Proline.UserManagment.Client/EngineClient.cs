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


        public IEnumerable<LinkedIdentity> PostPlayerIdentities( List<LinkedIdentity> list)
        {
            return _client.PostIdentitiesAsync(list).Result;
        }

        public UserAccount PostUserAccount( UserAccount userAccount)
        {
            return _client.PostUserAccountAsync(userAccount).Result;
        }

        public LinkedIdentity PostPlayerIdentity( LinkedIdentity LinkedIdentity)
        {
            return _client.PostIdentityAsync(LinkedIdentity).Result;
        }

        public SaveFile GetSaveFile(long id)
        {
            return _client.GetSaveFileAsync(id).Result;
        }

        public ICollection<SaveFile> GetSaveFiles()
        {
            return _client.GetSaveFilesAsync().Result;
        }

        public SaveFile PostSaveFile(SaveFile saveFile)
        {
            return _client.PostSaveFileAsync(saveFile).Result;
        }

        public void PutSaveFile(long id, SaveFile saveFile)
        {
            _client.PutSaveFileAsync(id, saveFile);
        }

        public IEnumerable<LinkedIdentity> GetIdentities( IEnumerable<string> enumerable)
        {
            return _client.GetAllMatchingIdentitiesAsync(enumerable).Result;
        }

        public PlayerAccount PostPlayerAccount( PlayerAccount playerAccount)
        {
            return _client.PostPlayerAccountAsync(playerAccount).Result;
        }

        public IEnumerable<UserDenial> GetUserDenies( long userId)
        {
            return _client.GetUserDenialsAsync(userId).Result;
        }

        public LinkedIdentity GetIdentity( string identifier)
        {
            return _client.GetMatchingIdentityAsync(identifier).Result;
        }

        public PlayerAccount GetPlayerAccount( long playerId)
        {
            return _client.GetPlayerAccountAsync(playerId).Result;
        }

        public UserAccount GetUserAccount( long id)
        {
            return _client.GetUserAccountAsync(id).Result;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
