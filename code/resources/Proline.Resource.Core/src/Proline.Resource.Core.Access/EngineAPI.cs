using Proline.Component.UserManagment;
using Proline.Proxies.UserManagment;
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

        public LoginPlayerOutParameter LoginPlayer(LoginPlayerInParameter inParameter)
        {
            if (inParameter == null || string.IsNullOrEmpty(inParameter.Identifier))
                return null;

            try
            {
                var identity = GetIdentity(inParameter.Identifier);
                if (identity == null)
                    return null;

                var denies = GetUserDenies(identity.UserId).OrderByDescending(e => e.ExpiresAt);
                var x = new LoginPlayerOutParameter()
                {
                    UserId = identity.UserId,
                    PlayerId = identity.PlayerId,
                    Deny = new UserDenyOutParameter(),
                };
                if (denies.Count() > 0)
                {
                    var deny = denies.First();
                    x.IsDenied = true;
                    x.Deny = new UserDenyOutParameter()
                    {
                        DenyId = deny.DenyId,
                        Reason = deny.Reason,
                        Untill = deny.ExpiresAt,
                    };
                }
                return x;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

        public RegistrationPlayerOutParameter RegisterPlayer(RegistrationPlayerInParameter inParameter)
        {

            if (inParameter == null || inParameter.Identifiers == null || inParameter.Identifiers.Count == 0)
                return null;

            try
            {
                var identities = inParameter.Identifiers;
                var primaryIdentity = identities.First();
                var identity = GetIdentity(primaryIdentity.Identifier);

                var playerAccount = new PlayerAccount()
                {
                    Name = inParameter.Username,
                    Priority = inParameter.Priority,
                    RegisteredAt = DateTime.UtcNow
                };

                var userAccount = new UserAccount()
                {
                    Username = inParameter.Username,
                    Priority = inParameter.Priority,
                    CreatedOn = DateTime.UtcNow,
                    GroupId = 0
                };


                if (identity == null)
                {
                    var x = GetIdentities(identities.Select(a => a.Identifier));
                    if (x.Count() > 0)
                    {
                        identity = x.First();
                        var playerId = PutPlayerAccount(playerAccount);
                        playerAccount = GetPlayerAccount(playerId.PlayerId);

                        var identityId = PutPlayerIdentity(new PlayerIndentity()
                        {
                            Identifier = primaryIdentity.Identifier,
                            IdentityTypeId = primaryIdentity.IdentitierType,
                            PlayerId = playerAccount.PlayerId,
                            UserId = identity.UserId
                        });

                        userAccount = GetUserAccount(identity.UserId);
                    }
                    else
                    {
                        var playerId = PutPlayerAccount(playerAccount);
                        var userId = PutUserAccount(userAccount);

                        var list = new List<PlayerIndentity>();

                        foreach (var item in identities)
                        {
                            list.Add(new PlayerIndentity()
                            {
                                Identifier = item.Identifier,
                                IdentityTypeId = item.IdentitierType,
                                PlayerId = playerAccount.PlayerId,
                                UserId = userAccount.UserId
                            });
                        }
                        PutPlayerIdentities(list); 
                    }
                }
                else
                {
                    return null;
                }

                var outParameters = new RegistrationPlayerOutParameter()
                {
                    Username = playerAccount.Name,
                    PlayerId = playerAccount.PlayerId,
                    Priority = playerAccount.Priority,
                    UserId = userAccount.UserId
                };
                return outParameters;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

        private IEnumerable<PlayerIndentity> PutPlayerIdentities(List<PlayerIndentity> list)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.PlayerIndentitiesPOSTAsync(list).Result;
            }
        }

        private UserAccount PutUserAccount(UserAccount userAccount)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.UserAccountsPOSTAsync(userAccount).Result;
            }
        }

        private PlayerIndentity PutPlayerIdentity(PlayerIndentity playerIndentity)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.PlayerIndentitiesPOSTAsync(playerIndentity).Result;
            }
        }

        private IEnumerable<PlayerIndentity> GetIdentities(IEnumerable<string> enumerable)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.PlayerIndentitiesAllAsync(enumerable).Result;
            }
        }

        private PlayerAccount PutPlayerAccount(PlayerAccount playerAccount)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.PlayerAccountsPOSTAsync(playerAccount).Result;
            }
        }

        private IEnumerable<UserDeny> GetUserDenies(long userId)
        { 
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.UserDeniesAll2Async(userId).Result;
            }
        }

        private PlayerIndentity GetIdentity(string identifier)
        { 
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.PlayerIndentitiesGET2Async(identifier).Result;
            }
        } 

        
         
        private PlayerAccount GetPlayerAccount(long playerId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.PlayerAccountsGETAsync(playerId).Result;
            }
        }

        private UserAccount GetUserAccount(long id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                return userCleint.UserAccountsGETAsync(id).Result;
            }
        }

        public void Dispose()
        {

        }
    }
}
