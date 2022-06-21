using Proline.DBAccess.Data;
using Proline.DBAccess.DBApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proline.DBAccess.MidLayer
{
    public partial class DBAccessApi
    {
        public GetPlayerResponse GetPlayer(GetPlayerRequest request)
        {
            var api = GetPlayerAPI.Execute(request);
            var response = api.response;
            response.ReturnCode = api.APIReturnCode;
            return response;
        }

        public RegisterPlayerResponse RegisterPlayer(RegisterPlayerRequest request)
        {
            var api =  RegisterPlayerAPI.Execute(request);
            var response = api.response;
            response.ReturnCode = api.APIReturnCode;
            return response;
        }
    }
}
