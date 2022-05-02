using NUnit.Framework;
using Proline.CentralEngine.NUnit;
using Proline.CentralEngine.NUnit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Tests.UserManagment.NUnit.Help
{
    public static class UserHelper
    {
        //public static string InsertRandomUser(out string username, out List<IdentifierInParameter> identitiers)
        //{
        //    username = Util.GenerateRandomString(15);
        //    identitiers = new List<IdentifierInParameter>()
        //    {
        //        new IdentifierInParameter()
        //        {
        //            Identifier = IdentityHelper.GenerateIdentifier(0),
        //            IdentitierType = 0,
        //        },
        //        new IdentifierInParameter()
        //        {
        //            Identifier = IdentityHelper.GenerateIdentifier(1),
        //            IdentitierType = 1,
        //        },
        //        new IdentifierInParameter()
        //        {
        //            Identifier = IdentityHelper.GenerateIdentifier(2),
        //            IdentitierType = 2,
        //        },
        //        new IdentifierInParameter()
        //        {
        //            Identifier = IdentityHelper.GenerateIdentifier(3),
        //            IdentitierType = 3,
        //        },
        //    };
        //    var inParams = new UserAccountInParameter()
        //    {
        //        Username = username,
        //        Identifiers = identitiers,
        //    };


        //    using (var httpClient = new HttpClient())
        //    {
        //        var _authHeader = new AuthenticationHeaderValue("Basic", "YWRtaW46UGEkJFdvUmQ=");
        //        Assert.DoesNotThrow(() =>
        //        {
        //            httpClient.DefaultRequestHeaders.Authorization = _authHeader;
        //            var userCleint = new Client("http://localhost:9703/", httpClient);
        //            userCleint.UserPOSTAsync(inParams).Wait();
        //        });
        //    }

        //    return username;
        //}
    }
}
