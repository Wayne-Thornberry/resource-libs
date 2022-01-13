using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Online.Data
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
}
