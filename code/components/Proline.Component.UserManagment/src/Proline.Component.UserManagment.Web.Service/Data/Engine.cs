using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Online.Data
{ 
    public class IdentityOutParameter
    {
        public long PlayerId { get; set; }
        public long UserId { get; set; }
        public int IdentifierType { get; set; }
        public string Identifier { get; set; }
    }

    public class UserDenyInParameter
    {
        public long UserId { get; set; }
        public string Reason { get; set; }
        public DateTime Untill { get; set; }
    }

    public class UserDenyOutParameter
    {
        public long DenyId { get; set; }
        public string Reason { get; set; }
        public DateTime Untill { get; set; }
    }

    public class UserAccountOutParameter
    { 
        public long UserId { get; set; }
        public string Username { get; set; }
    }

    public class UserAccountInParameter
    {
        public string Username { get; set; }
        public List<IdentifierInParameter> Identifiers { get; set; }
    }

    public class IdentifierInParameter
    {
        public long UserId { get; set; }
        public long PlayerId { get; set; }
        public string Identifier { get; set; }
        public int IdentitierType { get; set; }
    }
}
