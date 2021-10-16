using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Online.Data
{
    public class Header
    {
        public bool IsDebugMode { get; set; }
        public string Caller { get; set; }
    }

    public enum ReturnCode
    {
        Success,
        SystemError,
        PlayerNotFound,
        PlayerRegistrationFailed,
        PlayerCreationFailed,
        PlayerAlreadyExists,
        IdentityNotFound,
        IdentityFailedInsertion,
        IdentityAlreadyExists,
        InstanceKeyGenFailedUserHasKey,
        UserAlreadyExists,
        UserNotFound,
        AllowUserDenied,
        AllowUserAlreadyAllowed,
        InstanceFailedUserDenied,
        EngineNotInDebugMode,
        DataFileNotAdded,
        DataFileEntryNotAdded,
        ScriptUnableToStart, 
    }

    public enum IdentifierType
    {
        STEAM,
        IP,
        SOCIAL,
        DISCORD,
        EPIC,
        HWID,
    }

    public class PlayerOutParameter
    {
        public long PlayerId { get; set; }
        public string PlayerUsername { get; set; }
        public DateTime LastSeenDateTime { get; set; }
        public string Identifier1 { get; set; }
        public string Identifier2 { get; set; }
        public string Identifier3 { get; set; }
        public string Identifier4 { get; set; }
    }

    public class RegisterInstanceInParameter
    {
        public string InstanceName { get; set; }
        public long InstanceIdentityId { get; set; }
        public int MaxPlayers { get; set; }
    }

    public class InstanceDetailsOutParameter
    {
        public long InstanceId { get; set; }
    }

    public class UserDetailsOutParameter
    {
        public long UserId { get; set; }
    }

    public class UserInParameter
    {
        public string Username { get; set; }
    }

    public class PlayerDetailsOutParameter
    {
        public long PlayerId { get; set; }
    }
}
