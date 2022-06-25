using CitizenFX.Core;
using System;
using System.Collections.Generic; 

namespace Proline.ClassicOnline.PMGT
{
    // Handle errors on this level, dont let them get passed here
    public static class PlayerAccount
    {
        public static bool IsPlayerRegistered(Player player)
        {
            IEnumerable<string> identities;
            try
            {

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        public static bool IsPlayerDenied(Player player)
        {
            return true;
        }

        public static PlayerProfile GetPlayerProfile(Player player)
        {
            var details = new PlayerProfile();
            details.IsDenied = false;

            return details;
        }



        //        var identities = new List<IdentifierInParameter>
        //    {
        //        new IdentifierInParameter()
        //        {
        //            Identifier = player.Identifiers["steam"],
        //            IdentitierType = 0,
        //        },
        //        new IdentifierInParameter()
        //        {
        //            Identifier = player.Identifiers["ip"],
        //            IdentitierType = 1,
        //        },
        //        new IdentifierInParameter()
        //        {
        //            Identifier = player.Identifiers["discord"],
        //            IdentitierType = 2,
        //        }
        //    };

        //public static ReturnCode TryLoginPlayer(Player player)
        //{
        //    LoginPlayerInParameter inParameter = null;
        //    if (inParameter == null || string.IsNullOrEmpty(inParameter.Identifier))
        //        return ReturnCode.ParameterMissing;

        //    try
        //    {
        //        using (var httpClient = new EngineClient())
        //        {
        //            var identity = httpClient.GetIdentity(inParameter.Identifier);
        //            if (identity == null)
        //                return ReturnCode.SystemError;

        //            var denies = httpClient.GetUserDenies(identity.UserId).OrderByDescending(e => e.ExpiresAt);
        //            var x = new LoginPlayerOutParameter()
        //            {
        //                UserId = identity.UserId,
        //                PlayerId = identity.PlayerId,
        //                Deny = new UserDenyOutParameter(),
        //            };
        //            if (denies.Count() > 0)
        //            {
        //                var deny = denies.First();
        //                x.IsDenied = true;
        //                x.Deny = new UserDenyOutParameter()
        //                {
        //                    DenyId = deny.DenyId,
        //                    Reason = deny.Reason,
        //                    Untill = deny.ExpiresAt,
        //                };
        //            }
        //            return ReturnCode.Success;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return ReturnCode.SystemError;
        //        throw;
        //    }
        //}

        //public static ReturnCode TryRegisterPlayer(Player player)
        //{

        //    RegistrationPlayerInParameter inParameter = null;
        //    if (inParameter == null || inParameter.Identifiers == null || inParameter.Identifiers.Count == 0)
        //        return ReturnCode.ParameterMissing;

        //    try
        //    {
        //        using (var httpClient = new EngineClient())
        //        {

        //            var identities = inParameter.Identifiers;
        //            var primaryIdentity = identities.First();
        //            var identity = httpClient.GetIdentity(primaryIdentity.Identifier);

        //            var playerAccount = new Proxies.UserManagment.PlayerAccount()
        //            {
        //                Name = inParameter.Username,
        //                Priority = inParameter.Priority,
        //                RegisteredAt = DateTime.UtcNow
        //            };

        //            var userAccount = new UserAccount()
        //            {
        //                Username = inParameter.Username,
        //                Priority = inParameter.Priority,
        //                CreatedOn = DateTime.UtcNow,
        //                GroupId = 0
        //            };


        //            if (identity == null)
        //            {
        //                var x = httpClient.GetIdentities(identities.Select(a => a.Identifier));
        //                if (x.Count() > 0)
        //                {
        //                    identity = x.First();
        //                    var playerId = httpClient.PostPlayerAccount(playerAccount);
        //                    playerAccount = httpClient.GetPlayerAccount(playerId.PlayerId);

        //                    var identityId = httpClient.PostPlayerIdentity(new LinkedIdentity()
        //                    {
        //                        Identifier = primaryIdentity.Identifier,
        //                        IdentityTypeId = primaryIdentity.IdentitierType,
        //                        PlayerId = playerAccount.PlayerId,
        //                        UserId = identity.UserId
        //                    });

        //                    userAccount = httpClient.GetUserAccount(identity.UserId);
        //                }
        //                else
        //                {
        //                    var playerId = httpClient.PostPlayerAccount(playerAccount);
        //                    var userId = httpClient.PostUserAccount(userAccount);

        //                    var list = new List<LinkedIdentity>();

        //                    foreach (var item in identities)
        //                    {
        //                        list.Add(new LinkedIdentity()
        //                        {
        //                            Identifier = item.Identifier,
        //                            IdentityTypeId = item.IdentitierType,
        //                            PlayerId = playerAccount.PlayerId,
        //                            UserId = userAccount.UserId
        //                        });
        //                    }
        //                    httpClient.PostPlayerIdentities(list);
        //                }
        //            }
        //            else
        //            {
        //                return ReturnCode.RegistrationPlayerRegistered;
        //            }

        //            var outParameters = new RegistrationPlayerOutParameter()
        //            {
        //                Username = playerAccount.Name,
        //                PlayerId = playerAccount.PlayerId,
        //                Priority = playerAccount.Priority,
        //                UserId = userAccount.UserId
        //            };
        //            return ReturnCode.Success;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return ReturnCode.SystemError;
        //        //throw;
        //    }
        //}
    }
}
