using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Core.Data
{
    // ActionReason
    public enum ReturnCode : int
    {
        Success,
        SystemError,
        ParameterMissing,
        LoginFailedPlayerDenied,
        RegistrationPlayerRegistered,
    }
}
