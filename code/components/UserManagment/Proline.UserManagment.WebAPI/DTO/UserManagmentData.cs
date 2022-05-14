using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.WebService.DTO
{
    public class LoginPlayerInParameter
    {
        public string Identifier { get; set; }
    }

    public class LoginPlayerOutParameter
    {
        public long UserId { get; set; }
        public long PlayerId { get; set; }
        public bool IsDenied { get; set; }
        public UserDenyOutParameter Deny { get; set; }
    }

    public class RegistrationPlayerInParameter
    {
        public string Username { get; set; }
        public int GroupId { get; set; }
        public int Priority { get; set; }
        public List<IdentifierCreateInParameter> Identifiers { get; set; }
    }
    public class RegistrationPlayerOutParameter
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public long PlayerId { get; set; }
        public int Priority { get; set; }
    }

    public class IdentifierCreateInParameter
    {
        public long UserId { get; set; }
        public long PlayerId { get; set; }
        public string Identifier { get; set; }
        public int IdentitierType { get; set; }
    }

    public class IdentityOutParameter
    {
        public long PlayerId { get; set; }
        public long UserId { get; set; }
        public int IdentifierType { get; set; }
        public string Identifier { get; set; }
    }

    public class UserDenyOutParameter
    {
        public long DenyId { get; set; }
        public string Reason { get; set; }
        public DateTime Untill { get; set; }
    }

    public class PlayerAccountOutParameter
    {
        public long PlayerId { get; set; }
        public int Priority { get; set; }
    }

    public class UserAccountOutParameter
    {
        public long UserId { get; set; }
        public bool IsUserBanned { get; set; }
        public List<UserDenyOutParameter> Denies { get; set; }
        public List<PlayerAccountOutParameter> Players { get; set; }
        public List<IdentityOutParameter> Identities { get; set; }
    }
    public class UserDenial
    {
        public long DenyId { get; set; }
        public long DeniedByUserId { get; set; }
        public long UserId { get; set; }
        public DateTime BannedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Reason { get; set; }
    }
    public class UserGroup
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool APIAccess { get; set; }
    }
    public class UserInstanceLicence
    {
        public long InstanceIdentityId { get; set; }
        public string Key { get; set; }
        public long UserId { get; set; }
    }
    public class UserAccount
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public string Username { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class UserAllow
    {
        public long AllowId { get; set; }
        public long UserId { get; set; }
        public DateTime AllowedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Note { get; set; }
    }
    public class Tunable
    {
        public long TunableId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class NativeAPI
    {
        public long NativeId { get; set; }
        public string Name { get; set; }
        public bool HasReturn { get; set; }
        public string ReturnType { get; set; }
        public string Hash { get; set; }
        public int ArgCount { get; set; }
        public string Args { get; set; }
        public int Type { get; set; }
        public bool HasOutArgs { get; set; }
    }
    public class NativeAPICall
    {
        public long CallId { get; set; }
        public long NativeId { get; set; }
        public long CallerId { get; set; }
        public long TargetInstanceId { get; set; }
        public long TargetPlayerId { get; set; }
        public string ArgValues { get; set; }
        public string ReturnResult { get; set; }
        public int State { get; set; }
        public DateTime CallCreated { get; set; }
        public DateTime CallUpdated { get; set; }
        public DateTime CallCompleted { get; set; }
    }
    public class PlayerAccount
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
    public class PlayerIndentityType
    {
        public long IdentityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class SaveFile
    {
        public long FileId { get; set; }
        public long PlayerId { get; set; }
        public DateTime InsertedAt { get; set; }
        public string Data { get; set; }
    }
    public class Instance
    {
        public long InstanceId { get; set; }
        public string Name { get; set; } // Can be whatever a person wants
        public long InstanceIdentityId { get; set; }
        public string Type { get; set; }
        public DateTime LastSeenOnlineAt { get; set; }
        public string IpAddress { get; set; }
        public bool IsWhitelisted { get; set; }
        public int MaxPlayers { get; set; }
    }
    public class InstancePlayer
    {
        public long InstancePlayerId { get; set; }
        public long PlayerId { get; set; }
        public long InstanceId { get; set; }
        public DateTime LastSeenAt { get; set; }
    }
    public class InstanceUserAllow
    {
        public long InstanceAllowId { get; set; }
        public long UserId { get; set; }
        public DateTime AllowedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Note { get; set; }
    }
    public class InstanceUserDeny
    {
        public long InstanceDenyId { get; set; }
        public long UserId { get; set; }
        public DateTime BannedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Reason { get; set; }
    }
    public class LinkedIdentity
    {
        public long IdentityId { get; set; }
        public long PlayerId { get; set; }
        public int IdentityTypeId { get; set; }
        // if any of the identifiers match, then link that player with the user
        public long UserId { get; set; }
        public string Identifier { get; set; }
    }
}
