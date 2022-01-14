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
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                    var client = new Client("http://localhost:9703/", httpClient);
                    var identity = GetIdentity(client, inParameter.Identifier);
                    if (identity == null)
                        return null;

                    var denies = GetUserDenies(client, identity.UserId).OrderByDescending(e => e.ExpiresAt);
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
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                    var client = new Client("http://localhost:9703/", httpClient);

                    var identities = inParameter.Identifiers;
                    var primaryIdentity = identities.First();
                    var identity = GetIdentity(client, primaryIdentity.Identifier);

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
                        var x = GetIdentities(client, identities.Select(a => a.Identifier));
                        if (x.Count() > 0)
                        {
                            identity = x.First();
                            var playerId = PostPlayerAccount(client, playerAccount);
                            playerAccount = GetPlayerAccount(client, playerId.PlayerId);

                            var identityId = PostPlayerIdentity(client, new LinkedIdentity()
                            {
                                Identifier = primaryIdentity.Identifier,
                                IdentityTypeId = primaryIdentity.IdentitierType,
                                PlayerId = playerAccount.PlayerId,
                                UserId = identity.UserId
                            });

                            userAccount = GetUserAccount(client, identity.UserId);
                        }
                        else
                        {
                            var playerId = PostPlayerAccount(client, playerAccount);
                            var userId = PostUserAccount(client, userAccount);

                            var list = new List<LinkedIdentity>();

                            foreach (var item in identities)
                            {
                                list.Add(new LinkedIdentity()
                                {
                                    Identifier = item.Identifier,
                                    IdentityTypeId = item.IdentitierType,
                                    PlayerId = playerAccount.PlayerId,
                                    UserId = userAccount.UserId
                                });
                            }
                            PostPlayerIdentities(client, list);
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
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

        private IEnumerable<LinkedIdentity> PostPlayerIdentities(Client client, List<LinkedIdentity> list)
        {
            return client.PostIdentitiesAsync(list).Result;
        }

        private UserAccount PostUserAccount(Client client, UserAccount userAccount)
        {
            return client.PostUserAccountAsync(userAccount).Result;
        }

        private LinkedIdentity PostPlayerIdentity(Client client, LinkedIdentity LinkedIdentity)
        {
            return client.PostIdentityAsync(LinkedIdentity).Result;
        }

        private IEnumerable<LinkedIdentity> GetIdentities(Client client, IEnumerable<string> enumerable)
        {
            return client.GetAllMatchingIdentitiesAsync(enumerable).Result;
        }

        private PlayerAccount PostPlayerAccount(Client client, PlayerAccount playerAccount)
        {
            return client.PostPlayerAccountAsync(playerAccount).Result;
        }

        private IEnumerable<UserDenial> GetUserDenies(Client client, long userId)
        {
            return client.GetUserDenialsAsync(userId).Result;
        }

        private LinkedIdentity GetIdentity(Client client, string identifier)
        {
            return client.GetMatchingIdentityAsync(identifier).Result;
        } 
         
        private PlayerAccount GetPlayerAccount(Client client, long playerId)
        {
            return client.GetPlayerAccountAsync(playerId).Result;
        }

        private UserAccount GetUserAccount(Client client, long id)
        {
            return client.GetUserAccountAsync(id).Result;
        }

        public void Dispose()
        {

        }
    }
}
