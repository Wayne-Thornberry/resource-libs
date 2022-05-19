using Proline.CentralEngine.NUnit.Helpers;
using Proline.Proxies.UserManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.DBAccess.NUnit.Helpers
{
    public static class PlayerHelper
    {
        internal static RegistrationPlayerOutParameter RegisterNewPlayer(out List<IdentifierCreateInParameter> outParameters)
        {
            var username = Util.GenerateRandomString(15);
            var identity1 = IdentityHelper.GenerateIdentifier(0);
            var identity2 = IdentityHelper.GenerateIdentifier(1);
            var identity3 = IdentityHelper.GenerateIdentifier(2);
            var identity4 = IdentityHelper.GenerateIdentifier(3);
            RegistrationPlayerOutParameter outParameter = null;
            outParameters = new List<IdentifierCreateInParameter>() {
                        new IdentifierCreateInParameter()
                        {
                            Identifier = identity1,
                            IdentitierType = 0,
                        },
                        new IdentifierCreateInParameter()
                        {
                            Identifier = identity2,
                            IdentitierType = 1,
                        },
                        new IdentifierCreateInParameter()
                        {
                            Identifier = identity3,
                            IdentitierType = 2,
                        },
                        new IdentifierCreateInParameter()
                        {
                            Identifier = identity4,
                            IdentitierType = 3,
                        }
                    };

            UserAccountOutParameter user = null;

            using (var httpClient = new HttpClient())
            {
                var _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
                httpClient.DefaultRequestHeaders.Authorization = _authHeader;
                var userCleint = new Client("http://localhost:9703/", httpClient);
                outParameter = userCleint.RegisterPlayerAsync(new RegistrationPlayerInParameter()
                {
                    Username = username,
                    Identifiers = outParameters
                }).Result;

            }
            return outParameter;
        }
    }
}
