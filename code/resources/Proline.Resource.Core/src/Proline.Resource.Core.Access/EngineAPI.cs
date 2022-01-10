using Proline.Component.UserManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Proline.Resource.Core.Access
{
    public class EngineAPI : IDisposable
    {
        private AuthenticationHeaderValue _authHeader;

        public EngineAPI()
        { 
            _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        }

        public void PutPlayerIdentity(IdentifierInParameter identity)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                userCleint.IdentityPOSTAsync(identity).Wait();
            }
        }

        public void CreateNewUser(UserAccountInParameter identifiers)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                userCleint.UserPOSTAsync(identifiers).Wait();
            }
        }

        public IEnumerable<IdentityOutParameter> GetPlayerIdentities(List<IdentifierInParameter> identities)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.IdentityAllAsync(identities).Result;
            }
        }

        public UserDenyOutParameter GetUserDeny(long userId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.UserDenyGETAsync(userId).Result;
            }
        }

        public int ProcessPlayerConnection(string playerName, List<IdentifierInParameter> identities, out UserAccountOutParameter userAccount, out UserDenyOutParameter userDeny, out UserAccountOutParameter playerAccount, out object playerDeny)
        {
            userAccount = RetriveUserAccountOrCreateIfOneDoesntExist(playerName, identities, out var outParameter);

            // Returns a deny if there is an active deny on the player, will always return the one with the lengthest deny
            userDeny = GetUserDeny(userAccount.UserId);
            if (userDeny != null)
                return 2;
            playerAccount = RetrivePlayerAccountOrCreateIfOneDoesntExist(outParameter.Identifier);
        }

        private PlayerAccountParameter RetrivePlayerAccountOrCreateIfOneDoesntExist(string identifier)
        {

        }

        private UserAccountOutParameter RetriveUserAccountOrCreateIfOneDoesntExist(string playerName, List<IdentifierInParameter> identities, out IdentityOutParameter outParameter)
        {
            IdentityOutParameter identifier = null;
            outParameter = null;
            foreach (var item in identities)
            {
                identifier = GetPlayerIdentity(item.Identifier);
                if (identifier == null) continue;
                break;
            }

            if(identifier == null)
            {
                CreateNewUser(new UserAccountInParameter()
                {
                    Username = playerName,
                    Identifiers = identities,
                });
                outParameter = GetPlayerIdentity(identities[0].Identifier);
            }

            return GetUserAccount(identifier.UserId);
        }

        public bool IsUserDenied(object ban)
        {
            return ban == null;
        }

        public IdentityOutParameter GetPlayerIdentity(string identifier)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.IdentityGETAsync(identifier).Result;
            }
        }

        public UserAccountOutParameter GetUserAccount(long id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.UserGETAsync(id).Result;
            }
        }

        public void Dispose()
        {

        }
    }
}
